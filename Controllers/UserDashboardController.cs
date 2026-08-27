using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using MUNAdmin.Services;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace MUNAdmin.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly MUNAdminContext _context;
        private readonly UserServices _userServices;

        public UserDashboardController(MUNAdminContext context, UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        [Authorize(Policy = "DelegateOnly")]
        public async Task<IActionResult> Index(string council)
        {
            //Get info on user from Claim
            MUNInstance? currentMUNInstance = await _userServices.GetMUNFromClaimOrDefault(User);
            DelegationInstance? userDelegation = await _userServices.GetDelegationFromClaimOrDefault(User);

            //Error if no info on user from claim
            if (currentMUNInstance == null || userDelegation == null)
            {
                TempData["Error"] = "User login not connected to a target MUN instance or delegation instance";
                return RedirectToAction("Error", "Home");
            }

            //Generate a list of the councils the user is on
            List<string> participatingCouncilNames = new List<string>();
            for (int i = 0; i < userDelegation.CouncilList.Count(); i++)
            {
                participatingCouncilNames.Add(userDelegation.CouncilList[i].Council.CouncilName);
            }

            //Error if they are on no councils
            if (participatingCouncilNames.Count() == 0)
            {
                TempData["Error"] = "Could not find any councils attached to user";
                return RedirectToAction("Error", "Home");
            }

            //If no council was inputed into the controller, set it to a default council
            if (council == null)
            {
                council = participatingCouncilNames[0];
            }

            //Set the current council based on the input and error if it cant be found
            DelegationCouncil? currentCouncil = userDelegation.CouncilList.FirstOrDefault(x => x.Council.CouncilName == council);
            if (currentCouncil == null)
            {
                TempData["Error"] = "Can't find the specificed council";
                return RedirectToAction("Error", "Home");
            }

            //Send extra data to the view to properly display the dashboard
            ViewData["CurrentCouncil"] = council;
            ViewData["ParticipatingCouncils"] = participatingCouncilNames;
            ViewData["CurrentCouncilInformation"] = currentMUNInstance.CouncilInformationList.First(x => x.CouncilName == council);
            ViewData["MUNTitle"] = currentMUNInstance.MUNTitle;
            ViewData["DelegationInstance"] = userDelegation;

            return View(currentCouncil!);
        }

        [HttpPost]
        [Authorize(Policy = "DelegateOnly")]
        public async  Task<IActionResult> UserRequestRebutal(string desiredStateStr, string councilId)
        {
            bool desiredState = desiredStateStr == "value" ? true : false;
            //Debug.WriteLine(desiredState);

            //Gets their delegation instance if possible
            DelegationInstance? delegationInstance = await _userServices.GetDelegationFromClaimOrDefault(User);
            if (delegationInstance == null)
            {
                TempData["Error"] = "Can't find the specified MUN or delegation";
                return RedirectToAction("Error", "Home");
            }

            DelegationCouncil? delegationCouncil = delegationInstance.CouncilList.FirstOrDefault(x => x.Council.Id.ToString() == councilId);
            if (delegationCouncil == null)
            {
                TempData["Error"] = "Not a member of the specified council Id: " + councilId;
                return RedirectToAction("Error", "Home");
            }

            delegationCouncil.RequestedRebuttal = desiredState;
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using MUNAdmin.Services;
using System.Diagnostics;
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

        public IActionResult Index()
        {
            return View(_context.MUNInstance.First(x => x.AdminUsername == "ADMIN").DelegationList.First(x => x.DelegationCountry == "Canada") );
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
                return RedirectToAction(nameof(Index));
            }

            DelegationCouncil? delegationCouncil = delegationInstance.CouncilList.FirstOrDefault(x => x.Council.Id.ToString() == councilId);
            if (delegationCouncil == null)
            {
                TempData["Error"] = "Not a member of the specified council Id: " + councilId;
                return RedirectToAction(nameof(Index));
            }

            delegationCouncil.RequestedRebuttal = desiredState;
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}

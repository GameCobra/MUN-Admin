using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using MUNAdmin.Services;
using NuGet.Protocol;
using System.Diagnostics;
using System.Security.Claims;

namespace MUNAdmin.Controllers
{
    public class AdminViewController : Controller
    {
        private readonly MUNAdminContext _context;
        private readonly UserServices _userServices;

        public AdminViewController(MUNAdminContext context, UserServices userServices)
        {
            _context = context;
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ViewDelegations()
        {
            MUNInstance ownedMUNInstance = await _userServices.GetMUNFromClaimOrDefault(User)!;
            return View(ownedMUNInstance);
        }

        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ViewDelegationDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MUNInstance ownedMUNInstance = await _userServices.GetMUNFromClaimOrDefault(User)!;
            DelegationInstance? delegationToView = ownedMUNInstance.DelegationList.FirstOrDefault(x => x.Id == id);
            if (delegationToView == null)
            {
                return NotFound();
            }

            return View(delegationToView!);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult CreateDelegation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateDelegation([Bind("Id,DelegationCountry,DelegationAccsesCode")] DelegationInstance delegationInstanceToCreate)
        {
            //delegationInstanceToCreate.CouncilList = new List<DelegationCouncil>();
            if (ModelState.IsValid)
            {
                MUNInstance ownedMUNInstance = await _userServices.GetMUNFromClaimOrDefault(User)!;
                ownedMUNInstance.DelegationList.Add(delegationInstanceToCreate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewDelegations));
            }
            //ModelState.AddModelError() -- Add error at somepoint
            Debug.WriteLine(ModelState.ToJson());
            return View(delegationInstanceToCreate);
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ViewCouncilList()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using System.Security.Claims;

namespace MUNAdmin.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly MUNAdminContext _context;

        public UserDashboardController(MUNAdminContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.MUNInstance.First(x => x.AdminUsername == "ADMIN").DelegationList.First(x => x.DelegationCountry == "Canada") );
        }

        [HttpPost]
        [Authorize]
        public async  Task<IActionResult> UserRequestRebutal(bool desiredState, CouncilInformation council)
        {
            if (User.FindFirstValue(LoginClaims.AdminRole) == LoginClaims.DelegationRole)
            {
                return RedirectToAction(nameof(Index));
            }
            MUNInstance munInstance = await _context.MUNInstance.FirstAsync(x => x.MUNAccessCode.ToString() == User.FindFirstValue(LoginClaims.MUNID));
            DelegationInstance delegationInstance = munInstance.DelegationList.First(x => x.DelegationAccsesCode.ToString() == User.FindFirstValue(LoginClaims.DelegationID));

            return RedirectToAction(nameof(Index));
        }
    }
}

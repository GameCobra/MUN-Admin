using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using System.Security.Claims;

namespace MUNAdmin.Controllers
{
    public class LoginController : Controller
    {
        private readonly MUNAdminContext _context;

        public LoginController(MUNAdminContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAdmin(AdminLoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            MUNInstance? AdminMUN = await _context.MUNInstance.FirstOrDefaultAsync(x => x.AdminUsername == model.AdminUsername && x.AdminPassword == model.AdminPassword);

            if (AdminMUN == null)
            {
                ModelState.AddModelError("Invalid Login", "The information you inputed does not corraspond to a valid account");
                return View(model);
            }


            var claims = new List<Claim>
            {
                new Claim(LoginClaims.MUNID, AdminMUN!.Id.ToString()),
                new Claim(LoginClaims.Role, LoginClaims.AdminRole)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LoginDelegate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginDelegate(DelegateLoginModel model)
        {
            //If the user did not input all the information required
            if (!ModelState.IsValid)
                return View(model);

            //Check if the MUN level Access code corasponds to anything
            MUNInstance? userMUN = await _context.MUNInstance.FirstOrDefaultAsync(x => x.MUNAccessCode == model.MUNAccessCode);

            if (userMUN == null)
            {
                ModelState.AddModelError("Invalid Login", "The information you inputed does not corraspond to a valid account");
                return View(model);
            }

            //Check if the Delegation level access code corasponds to a delegation
            DelegationInstance? userDelegation = userMUN!.DelegationList.FirstOrDefault(x => x.DelegationAccsesCode == model.DelegationAccsesCode);

            if (userDelegation == null)
            {
                ModelState.AddModelError("Invalid Login", "The information you inputed does not corraspond to a valid account");
                return View(model);
            }

            //Create the claim cookie
            var claims = new List<Claim>
            {
                new Claim(LoginClaims.MUNID, userMUN!.Id.ToString()),
                new Claim(LoginClaims.DelegationID, userDelegation!.Id.ToString()),
                new Claim(LoginClaims.Role, LoginClaims.DelegationRole)
            };

            //Give the user the claim cookie
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SelectLoginMethod()
        {
            return View();
        }

    }
}

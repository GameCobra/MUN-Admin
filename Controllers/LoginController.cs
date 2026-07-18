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
        public IActionResult Login()
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
                new Claim("MUN ID", AdminMUN!.Id.ToString()),
                new Claim("Role", "Admin")
            };

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
    }
}

using Microsoft.AspNetCore.Mvc;
using MUNAdmin.Data;

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
    }
}

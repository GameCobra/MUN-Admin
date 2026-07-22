using Microsoft.EntityFrameworkCore;
using MUNAdmin.Data;
using MUNAdmin.Models;
using MUNAdmin.Models.LoginModels;
using System.Security.Claims;

namespace MUNAdmin.Services
{
    public class UserServices
    {
        private readonly MUNAdminContext _context;

        public UserServices(MUNAdminContext context)
        {
            _context = context;
        }

        public async Task<MUNInstance?> GetClaimMUNInstanceOrDefault(ClaimsPrincipal User)
        {
            MUNInstance? munInstance = await _context.MUNInstance.FirstOrDefaultAsync(x => x.Id.ToString() == User.FindFirstValue(LoginClaims.MUNID));
            return munInstance;
        }

        public async Task<DelegationInstance?> GetClaimDelegationInstanceOrDefault(ClaimsPrincipal User)
        {
            MUNInstance? munInstance = await GetClaimMUNInstanceOrDefault(User);
            if (munInstance == null)
                return null;
            DelegationInstance? delegationInstance = munInstance.DelegationList.FirstOrDefault(x => x.Id.ToString() == User.FindFirstValue(LoginClaims.DelegationID));
            return delegationInstance;
        }

    }
}

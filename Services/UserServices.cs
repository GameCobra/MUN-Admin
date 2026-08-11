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

        public async Task<MUNInstance?> GetMUNFromClaimOrDefault(ClaimsPrincipal User)
        {
            MUNInstance? munInstance = await _context.MUNInstance.FirstOrDefaultAsync(x => x.Id.ToString() == User.FindFirstValue(LoginClaims.MUNID));
            return munInstance;
        }

        //Returns the a delegation object based 
        public async Task<DelegationInstance?> GetDelegationFromClaimOrDefault(ClaimsPrincipal User)
        {
            MUNInstance? munInstance = await GetMUNFromClaimOrDefault(User);
            if (munInstance == null)
                return null;
            DelegationInstance? delegationInstance = munInstance.DelegationList.FirstOrDefault(x => x.Id.ToString() == User.FindFirstValue(LoginClaims.DelegationID));
            return delegationInstance;
        }

        public Boolean IsClaimAdmin(ClaimsPrincipal User)
        {
            return User.FindFirstValue(LoginClaims.Role) == LoginClaims.AdminRole;
        }
        public Boolean IsClaimDelegation(ClaimsPrincipal User)
        {
            return User.FindFirstValue(LoginClaims.Role) == LoginClaims.DelegationRole;
        }

    }
}

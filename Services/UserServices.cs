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

        public List<List<Ammendment>> GetAllAmmendmentsOnCouncil(CouncilInformation council, MUNInstance munInstance)
        {
            List<List<Ammendment>> ammendmentsOnEachResolution = new List<List<Ammendment>>();
            foreach (Resolution resolution in council.Resolutions)
            {
                List<Ammendment>? ammendmentsOnSingleResolution = GetAllAmmendmentsOnResolution(resolution, council, munInstance);
                if (ammendmentsOnSingleResolution == null)
                {
                    continue;
                }
                ammendmentsOnEachResolution.Add(ammendmentsOnSingleResolution);
            }
            return ammendmentsOnEachResolution;

        }

        public List<Ammendment> GetAllAmmendmentsOnResolution(Resolution resolution, CouncilInformation council, MUNInstance munInstance)
        {
            List<Ammendment> ammendmentsOnResolution = new List<Ammendment>();


            // Searches through every delegation 
            foreach (DelegationInstance delegation in munInstance.DelegationList)
            {
                // Gets the provided council if possible
                DelegationCouncil? delegationCouncil = delegation.CouncilList.FirstOrDefault(x => x.Council.CouncilName == council.CouncilName);
                if (delegationCouncil == null)
                {
                    break;
                }

                //And returns any ammendments that match the current resolution
                foreach (Ammendment ammendment in delegationCouncil.Ammendments)
                {
                    if (ammendment.ResolutionID == resolution.Id)
                    {
                        ammendmentsOnResolution.Add(ammendment);
                    }
                }
            }
            return ammendmentsOnResolution;
        }

        public List<String> GetDelegationsCouncilNames(DelegationInstance delegation)
        {
            //Generate a list of the councils the user is on
            List<string> participatingCouncilNames = new List<string>();
            for (int i = 0; i < delegation.CouncilList.Count(); i++)
            {
                participatingCouncilNames.Add(delegation.CouncilList[i].Council.CouncilName);
            }

            return participatingCouncilNames;
        }

    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using MUNAdmin.Models.LoginModels;
using System.Security.Claims;

namespace MUNAdmin.Policies
{
    public class IsAdminOfMUNHandler : AuthorizationHandler<IsAdminOfMUN>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IsAdminOfMUN requirement)
        {
            bool isAdminAccount = context.User.FindFirstValue(LoginClaims.Role) == LoginClaims.AdminRole;
            if (!isAdminAccount)
            {
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}

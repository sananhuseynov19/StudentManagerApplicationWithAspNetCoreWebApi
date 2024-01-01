using Microsoft.AspNetCore.Authorization;
using StudentManagerApplicationWithWebApi.Requirments;
using System.Security.Claims;

namespace StudentManagerApplicationWithWebApi.RequirementsHandler
{
    public class IsAdminHandler : AuthorizationHandler<StudentUpdateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentUpdateRequirement requirement)
        {
            if(context.User.HasClaim(ClaimTypes.Role,"Sabina"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using StudentManagerApplicationWithWebApi.Requirments;
using System.Security.Claims;

namespace StudentManagerApplicationWithWebApi.RequirementsHandler
{
    public class AgeHandler : AuthorizationHandler<StudentUpdateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentUpdateRequirement requirement)
        {
            var ageClaim = context.User.FindFirst("Age");

            if(ageClaim!=null && int.TryParse(ageClaim.Value,out int Age))
            {
                if(Age>=21)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}

using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class StudentTaskManipulationAuthorizationHandler : AuthorizationHandler<StudentTaskManipulationRequirement>
    {
        private readonly IStudentTasksRepository _studentTasksRepository;

        public StudentTaskManipulationAuthorizationHandler(IStudentTasksRepository studentTasksRepository)
        {
            _studentTasksRepository = studentTasksRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentTaskManipulationRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int studentTaskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out studentTaskId)) return;
                var hasPermision = await _studentTasksRepository.HasPermission(Guid.Parse(ClaimUserId.Value), studentTaskId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class StudentTaskManipulationRequirement : IAuthorizationRequirement { }
}

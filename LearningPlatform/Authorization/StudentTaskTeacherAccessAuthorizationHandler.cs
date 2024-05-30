using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class StudentTaskTeacherAccessAuthorizationHandler : AuthorizationHandler<StudentTaskTeacherAccessRequirement>
    {
        private readonly IStudentTasksRepository _studentTasksRepository;

        public StudentTaskTeacherAccessAuthorizationHandler(IStudentTasksRepository studentTasksRepository)
        {
            _studentTasksRepository = studentTasksRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentTaskTeacherAccessRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int studentTaskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out studentTaskId)) return;
                var hasAccess = await _studentTasksRepository.HasTeacherAccess(Guid.Parse(ClaimUserId.Value), studentTaskId);
                if (hasAccess) context.Succeed(requirement);
            }
            return;
        }
    }

    public class StudentTaskTeacherAccessRequirement : IAuthorizationRequirement { }
}

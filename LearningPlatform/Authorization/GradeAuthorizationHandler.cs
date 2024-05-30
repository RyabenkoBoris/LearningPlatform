using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class GradeAuthorizationHandler : AuthorizationHandler<GradeRequirement>
    {
        private readonly IGradesRepository _gradesRepository;

        public GradeAuthorizationHandler(IGradesRepository gradesRepository)
        {
            _gradesRepository = gradesRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, GradeRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int gradeId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out gradeId)) return;
                var hasAccess = await _gradesRepository.IsTeacher(Guid.Parse(ClaimUserId.Value), gradeId);
                if (hasAccess) context.Succeed(requirement);
            }
            return;
        }
    }

    public class GradeRequirement : IAuthorizationRequirement { }
}

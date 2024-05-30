using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace LearningPlatform.Authorization
{
    public class CourseAuthorizationHandler : AuthorizationHandler<OwnerTeachersRequirement>
    {
        private readonly ICoursesRepository _coursesRepository;
        public CourseAuthorizationHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerTeachersRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int groupId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out groupId)) return;
                var isGroupOwner = await _coursesRepository.IsGroupOwner(Guid.Parse(ClaimUserId.Value), groupId);
                var isOneOfTeacher = await _coursesRepository.IsOneOfTeacher(Guid.Parse(ClaimUserId.Value), groupId);
                if (isGroupOwner || isOneOfTeacher) context.Succeed(requirement);
            }
            return;
        }
    }
    public class OwnerTeachersRequirement : IAuthorizationRequirement { }
}

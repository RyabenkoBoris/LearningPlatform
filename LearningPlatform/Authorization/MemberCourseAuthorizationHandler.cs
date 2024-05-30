using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class MemberCourseAuthorizationHandler : AuthorizationHandler<MemberCourseRequirement>
    {
        private readonly ICoursesRepository _coursesRepository;

        public MemberCourseAuthorizationHandler(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MemberCourseRequirement requirement)
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
                var isStudentInGroup = await _coursesRepository.IsStudentInGroup(Guid.Parse(ClaimUserId.Value), groupId);
                if (isGroupOwner || isOneOfTeacher || isStudentInGroup) context.Succeed(requirement);
            }
            return;
        }
    }
    public class MemberCourseRequirement : IAuthorizationRequirement { }
}

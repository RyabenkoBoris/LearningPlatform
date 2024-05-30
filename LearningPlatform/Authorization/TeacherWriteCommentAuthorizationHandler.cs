using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class TeacherWriteCommentAuthorizationHandler : AuthorizationHandler<TeacherWriteCommentRequirement>
    {
        private readonly ICommentsRepository _commentsRepository;

        public TeacherWriteCommentAuthorizationHandler(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TeacherWriteCommentRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int studentTaskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.Request.Form["StudentTaskId"];
                if (ClaimUserId == null || StringValues.IsNullOrEmpty(routeId)) return;
                if (!int.TryParse(routeId.ToString(), out studentTaskId)) return;
                var hasPermision = await _commentsRepository.TeacherWriteAccess(Guid.Parse(ClaimUserId.Value), studentTaskId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class TeacherWriteCommentRequirement : IAuthorizationRequirement { }
}

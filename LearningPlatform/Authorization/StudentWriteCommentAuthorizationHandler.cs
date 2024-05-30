using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class StudentWriteCommentAuthorizationHandler : AuthorizationHandler<StudentWriteCommentRequirement>
    {
        private readonly ICommentsRepository _commentsRepository;

        public StudentWriteCommentAuthorizationHandler(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentWriteCommentRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int studentTaskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.Request.Form["StudentTaskId"];
                if (ClaimUserId == null || StringValues.IsNullOrEmpty(routeId)) return;
                if (!int.TryParse(routeId.ToString(), out studentTaskId)) return;
                var hasPermision = await _commentsRepository.StudentWriteAccess(Guid.Parse(ClaimUserId.Value), studentTaskId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class StudentWriteCommentRequirement : IAuthorizationRequirement { }
}

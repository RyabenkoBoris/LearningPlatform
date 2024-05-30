using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class DeleteCommentAuthorizationHandler : AuthorizationHandler<DeleteCommentRequirement>
    {
        private readonly ICommentsRepository _commentsRepository;

        public DeleteCommentAuthorizationHandler(ICommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteCommentRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int fileId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out fileId)) return;
                var hasPermision = await _commentsRepository.HasPermissionToDelete(Guid.Parse(ClaimUserId.Value), fileId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class DeleteCommentRequirement : IAuthorizationRequirement { }
}

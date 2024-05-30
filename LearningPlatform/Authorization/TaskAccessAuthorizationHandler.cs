using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class TaskAccessAuthorizationHandler : AuthorizationHandler<TaskAccessRequirement>
    {
        private readonly ITasksRepository _tasksRepository;

        public TaskAccessAuthorizationHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TaskAccessRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int taskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out taskId)) return;
                var hasAccess = await _tasksRepository.HasAccess(Guid.Parse(ClaimUserId.Value), taskId);
                if (hasAccess) context.Succeed(requirement);
            }
            return;
        }
    }

    public class TaskAccessRequirement : IAuthorizationRequirement { }
}

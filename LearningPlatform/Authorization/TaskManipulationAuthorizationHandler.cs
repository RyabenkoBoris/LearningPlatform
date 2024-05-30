using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class TaskManipulationAuthorizationHandler : AuthorizationHandler<TaskManipulationRequirement>
    {
        private readonly ITasksRepository _tasksRepository;

        public TaskManipulationAuthorizationHandler(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TaskManipulationRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int taskId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out taskId)) return;
                var hasPermision = await _tasksRepository.HasPermissionToChange(Guid.Parse(ClaimUserId.Value), taskId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class TaskManipulationRequirement : IAuthorizationRequirement { }
}

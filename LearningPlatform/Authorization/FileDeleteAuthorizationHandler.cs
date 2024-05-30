using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class FileDeleteAuthorizationHandler : AuthorizationHandler<FileDeleteRequirement>
    {
        private readonly IFilesRepository _filesRepository;

        public FileDeleteAuthorizationHandler(IFilesRepository filesRepository)
        {
            _filesRepository = filesRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FileDeleteRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int fileId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out fileId)) return;
                var hasPermision = await _filesRepository.HasPermissionToDelete(Guid.Parse(ClaimUserId.Value), fileId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class FileDeleteRequirement : IAuthorizationRequirement { }
}

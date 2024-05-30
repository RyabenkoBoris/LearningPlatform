using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class SectionManipulationAuthorizationHandler : AuthorizationHandler<SectionManipularionRequirement>
    {
        private readonly ISectionsRepository _sectionsRepository;

        public SectionManipulationAuthorizationHandler(ISectionsRepository sectionsRepository)
        {
            _sectionsRepository = sectionsRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SectionManipularionRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int sectionId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out sectionId)) return;
                var hasPermision = await _sectionsRepository.HasPermissionToChange(Guid.Parse(ClaimUserId.Value), sectionId);
                if (hasPermision) context.Succeed(requirement);
            }
            return;
        }
    }
    public class SectionManipularionRequirement : IAuthorizationRequirement { }
}

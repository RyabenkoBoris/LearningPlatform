using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class JournalTeacherAccessAuthorizationHandler : AuthorizationHandler<JournalTeacherAccessRequirement>
    {
        private readonly IJournalRepository _journalRepository;

        public JournalTeacherAccessAuthorizationHandler(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JournalTeacherAccessRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int journalId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.GetRouteValue("id");
                if (ClaimUserId == null || routeId == null) return;
                if (!int.TryParse(routeId.ToString(), out journalId)) return;
                var hasAccess = await _journalRepository.HasTeacherAccess(Guid.Parse(ClaimUserId.Value), journalId);
                if (hasAccess) context.Succeed(requirement);
            }
            return;
        }
    }

    public class JournalTeacherAccessRequirement : IAuthorizationRequirement { }
}

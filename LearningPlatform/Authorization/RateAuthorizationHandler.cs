using LearningPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace LearningPlatform.Authorization
{
    public class RateAuthorizationHandler : AuthorizationHandler<RateRequirement>
    {
        private readonly IJournalRepository _journalRepository;

        public RateAuthorizationHandler(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RateRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                int journalId;
                var ClaimUserId = httpContext.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier);
                var routeId = httpContext.Request.Form["JournalId"];
                if (ClaimUserId == null || StringValues.IsNullOrEmpty(routeId)) return;
                if (!int.TryParse(routeId, out journalId)) return;
                var hasAccess = await _journalRepository.HasTeacherAccess(Guid.Parse(ClaimUserId.Value), journalId);
                if (hasAccess) context.Succeed(requirement);
            }
            return;
        }
    }

    public class RateRequirement : IAuthorizationRequirement { }
}

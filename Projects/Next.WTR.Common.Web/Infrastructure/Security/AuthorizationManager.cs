namespace Next.WTR.Common.Web.Infrastructure.Security
{
    using System.Collections.Immutable;
    using System.Linq;
    using System.Security.Claims;
    using Next.WTR.Common.IoC;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Types;

    public sealed class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var action = context.Action.First(claim => claim.Type == Common.Infrastructure.Security.ClaimTypes.ActionName).Value;
            var actionParameters = string.Join(", ", context.Action.Where(claim => claim.Type == Common.Infrastructure.Security.ClaimTypes.ActionParameter).Select(claim => claim.Value));
            var resource = context.Resource.First().Value;
            var resourceWithAction = $"{resource}/{action}";
            var resourceWithActionAndParameters = string.IsNullOrEmpty(actionParameters) ? resourceWithAction : $"{resourceWithAction}({actionParameters})";
            var roles = context.Principal.Claims.Where(claim => claim.Type == System.Security.Claims.ClaimTypes.Role).Select(claim => (NonEmptyLowerCaseString)claim.Value).ToImmutableList();
            var accessResolver = IoCContainerProvider.GetContainer().Get<IAccessResolver>();
            return accessResolver.CanAccess((NonEmptyLowerCaseString)resourceWithActionAndParameters, context.Principal.Identity.IsAuthenticated, roles);
        }
    }
}
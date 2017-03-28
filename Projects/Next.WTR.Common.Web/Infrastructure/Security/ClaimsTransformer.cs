namespace Next.WTR.Common.Web.Infrastructure.Security
{
    using System.Linq;
    using System.Security.Claims;
    using log4net;
    using Next.WTR.Common.IoC;
    using Next.WTR.Common.Log4Net;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Types;

    public sealed class ClaimsTransformer : ClaimsAuthenticationManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ClaimsTransformer));

        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            return !incomingPrincipal.Identity.IsAuthenticated ? base.Authenticate(resourceName, incomingPrincipal) : CreatePrincipal(incomingPrincipal);
        }

        private static ClaimsPrincipal CreatePrincipal(ClaimsPrincipal principal)
        {
            var sessionIdClaim = principal.Claims.FirstOrDefault(claim => claim.Type == Common.Infrastructure.Security.ClaimTypes.SessionId);

            var sessionId = sessionIdClaim?.Value;

            if (!string.IsNullOrEmpty(sessionId))
            {
                var facade = IoCContainerProvider.GetContainer().Get<IClaimsPrincipalProvider>();

                var result = facade.GetClaimsPrincipal((NonEmptyString)sessionId);

                if (!result.IsFailure)
                {
                    return result.Value;
                }

                Logger.Error(() => result.Error);
                return null;
            }

            return null;
        }
    }
}
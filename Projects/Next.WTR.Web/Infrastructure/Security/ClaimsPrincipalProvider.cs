namespace Next.WTR.Web.Infrastructure.Security
{
    using System.Security.Claims;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Logic.Facades.Shared;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class ClaimsPrincipalProvider : IClaimsPrincipalProvider
    {
        private readonly GetClaimsPrincipalBySessionIdFacade _getClaimsPrincipalBySessionIdFacade;

        public ClaimsPrincipalProvider(GetClaimsPrincipalBySessionIdFacade getClaimsPrincipalBySessionIdFacade)
        {
            _getClaimsPrincipalBySessionIdFacade = getClaimsPrincipalBySessionIdFacade;
        }

        public IResult<ClaimsPrincipal, Error> GetClaimsPrincipal(NonEmptyString sessionId)
        {
            return _getClaimsPrincipalBySessionIdFacade.GetClaimsPrincipal(sessionId);
        }
    }
}
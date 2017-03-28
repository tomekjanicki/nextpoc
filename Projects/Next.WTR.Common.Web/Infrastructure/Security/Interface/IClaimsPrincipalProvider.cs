namespace Next.WTR.Common.Web.Infrastructure.Security.Interface
{
    using System.Security.Claims;
    using Next.WTR.Common.Shared;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IClaimsPrincipalProvider
    {
        IResult<ClaimsPrincipal, Error> GetClaimsPrincipal(NonEmptyString sessionId);
    }
}
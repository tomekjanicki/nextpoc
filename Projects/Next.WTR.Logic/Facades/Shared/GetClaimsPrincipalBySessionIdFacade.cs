namespace Next.WTR.Logic.Facades.Shared
{
    using System.Collections.Immutable;
    using System.Linq;
    using System.Security.Claims;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.User.GetData;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Thinktecture.IdentityModel;

    public sealed class GetClaimsPrincipalBySessionIdFacade
    {
        private readonly IMediator _mediator;
        private readonly IUserGetDataQueryFactory _userGetDataQueryFactory;

        public GetClaimsPrincipalBySessionIdFacade(IMediator mediator, IUserGetDataQueryFactory userGetDataQueryFactory)
        {
            _mediator = mediator;
            _userGetDataQueryFactory = userGetDataQueryFactory;
        }

        public IResult<ClaimsPrincipal, Error> GetClaimsPrincipal(NonEmptyString sessionId)
        {
            var queryResult = _userGetDataQueryFactory.Get(sessionId);

            return queryResult.OnSuccess(() => GetResult(sessionId, queryResult.Value), Error.CreateGeneric);
        }

        private static IResult<ClaimsPrincipal, Error> GetClaimsPrincipal(UserData userData, NonEmptyString sessionId)
        {
            var claimsPrincipal = Principal.Create("Application", new Claim(ClaimTypes.Name, userData.UserName), new Claim(ClaimTypes.NameIdentifier, userData.UserId.Value.ToString()), new Claim(Common.Infrastructure.Security.ClaimTypes.SessionId, sessionId.Value));

            userData.Groups.ToImmutableList().ForEach(role => claimsPrincipal.Identities.First().AddClaim(new Claim(ClaimTypes.Role, role)));

            return Result<ClaimsPrincipal, Error>.Ok(claimsPrincipal);
        }

        private IResult<ClaimsPrincipal, Error> GetResult(NonEmptyString sessionId, Query query)
        {
            var result = _mediator.Send(query);

            return result.HasNoValue ? ((NonEmptyString)("User with session id " + sessionId + " has not been found")).ToNotFound<ClaimsPrincipal>() : GetClaimsPrincipal(result.Value, sessionId);
        }
    }
}

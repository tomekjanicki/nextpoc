namespace Next.WTR.Logic.Facades.Apis
{
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Security.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class AccountLogoutFacade
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        private readonly IUserDeleteSessionCommandFactory _userDeleteSessionCommandFactory;

        public AccountLogoutFacade(IAuthenticationService authenticationService, IMediator mediator, IUserDeleteSessionCommandFactory userDeleteSessionCommandFactory)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
            _userDeleteSessionCommandFactory = userDeleteSessionCommandFactory;
        }

        public IResult<Error> Logout(string sessionId)
        {
            var commandResult = _userDeleteSessionCommandFactory.TryCreate(sessionId);

            return Helper.Execute(_mediator, commandResult, command => _authenticationService.SignOut());
        }
    }
}
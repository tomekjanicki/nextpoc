namespace Next.WTR.Logic.Facades.Apis
{
    using System;
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Security.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.User.CreateSession;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Account.Login;

    public sealed class AccountLoginFacade
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;
        private readonly IUserCreateSessionCommandFactory _createSessionCommandFactory;

        public AccountLoginFacade(IAuthenticationService authenticationService, IMediator mediator, IUserCreateSessionCommandFactory createSessionCommandFactory)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
            _createSessionCommandFactory = createSessionCommandFactory;
        }

        public IResult<Error> Login(RequestUserIdAndPassword requestUserIdAndPassword)
        {
            var commandResult = _createSessionCommandFactory.Get(requestUserIdAndPassword.UserId, requestUserIdAndPassword.Password);

            return Helper.Execute<Command, Guid>(_mediator, commandResult, (command, sessionId) => _authenticationService.SignIn((NonEmptyString)sessionId.ToString()));
        }
    }
}

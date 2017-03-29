namespace Next.WTR.Logic.Facades.Apis
{
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

            return commandResult.OnSuccess(() => GetResult(commandResult.Value), Error.CreateGeneric);
        }

        private IResult<Error> GetResult(Command command)
        {
            var result = _mediator.Send(command);

            return result.IsSuccess ? Result<Error>.Ok().Tee(r => _authenticationService.SignIn((NonEmptyString)result.Value.ToString())) : result;
        }
    }
}

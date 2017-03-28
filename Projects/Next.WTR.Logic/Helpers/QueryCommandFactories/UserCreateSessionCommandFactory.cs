namespace Next.WTR.Logic.Helpers.QueryCommandFactories
{
    using Next.WTR.Logic.CQ.User.CreateSession;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Account.Login;

    public sealed class UserCreateSessionCommandFactory : IUserCreateSessionCommandFactory
    {
        public IResult<Command, NonEmptyString> Get(Data data)
        {
            return Command.TryCreate(data);
        }
    }
}
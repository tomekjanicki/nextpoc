namespace Next.WTR.Logic.Helpers.QueryCommandFactories
{
    using Next.WTR.Logic.CQ.User.CreateSession;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class UserCreateSessionCommandFactory : IUserCreateSessionCommandFactory
    {
        public IResult<Command, NonEmptyString> Get(string userId, string password)
        {
            return Command.TryCreate(userId, password);
        }
    }
}
namespace Next.WTR.Logic.Helpers.QueryCommandFactories
{
    using Next.WTR.Logic.CQ.User.DeleteSession;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class UserDeleteSessionCommandFactory : IUserDeleteSessionCommandFactory
    {
        public IResult<Command, NonEmptyString> TryCreate(string sessionId)
        {
            return Command.TryCreate(sessionId);
        }
    }
}
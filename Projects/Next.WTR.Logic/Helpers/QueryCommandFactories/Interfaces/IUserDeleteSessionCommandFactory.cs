namespace Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces
{
    using Next.WTR.Logic.CQ.User.DeleteSession;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IUserDeleteSessionCommandFactory
    {
        IResult<Command, NonEmptyString> TryCreate(string sessionId);
    }
}
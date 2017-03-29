namespace Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces
{
    using Next.WTR.Logic.CQ.User.CreateSession;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IUserCreateSessionCommandFactory
    {
        IResult<Command, NonEmptyString> Get(string userId, string password);
    }
}
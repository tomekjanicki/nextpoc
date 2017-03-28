namespace Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces
{
    using Next.WTR.Logic.CQ.User.GetData;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IUserGetDataQueryFactory
    {
        IResult<Query, NonEmptyString> Get(string sessionId);
    }
}
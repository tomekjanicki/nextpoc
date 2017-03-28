namespace Next.WTR.Logic.Helpers.QueryCommandFactories
{
    using Next.WTR.Logic.CQ.User.GetData;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class UserGetDataQueryIdQueryFactory : IUserGetDataQueryFactory
    {
        public IResult<Query, NonEmptyString> Get(string sessionId)
        {
            return Query.TryCreate(sessionId);
        }
    }
}

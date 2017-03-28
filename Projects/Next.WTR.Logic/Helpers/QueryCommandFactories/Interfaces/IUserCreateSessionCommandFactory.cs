namespace Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces
{
    using Next.WTR.Logic.CQ.User.CreateSession;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Account.Login;

    public interface IUserCreateSessionCommandFactory
    {
        IResult<Command, NonEmptyString> Get(Data data);
    }
}
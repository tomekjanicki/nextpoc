namespace Next.WTR.Common.Shared.TemplateMethods.Queries.Interfaces
{
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IGetRepository<T>
        where T : class
    {
        Maybe<T> Get(PositiveInt id);
    }
}
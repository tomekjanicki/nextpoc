namespace Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces
{
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IDeleteRepository
    {
        bool ExistsById(PositiveInt id);

        Maybe<NonEmptyString> GetRowVersionById(PositiveInt id);

        void Delete(PositiveInt id);
    }
}
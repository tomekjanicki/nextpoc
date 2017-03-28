namespace Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces
{
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IUpdateRepository<in T>
    {
        bool ExistsById(PositiveInt id);

        Maybe<NonEmptyString> GetRowVersionById(PositiveInt id);

        void Update(T command);
    }
}
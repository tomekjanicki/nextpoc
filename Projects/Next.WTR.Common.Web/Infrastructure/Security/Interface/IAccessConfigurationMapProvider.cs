namespace Next.WTR.Common.Web.Infrastructure.Security.Interface
{
    using System.Collections.Immutable;
    using Next.WTR.Types;

    public interface IAccessConfigurationMapProvider
    {
        ImmutableDictionary<NonEmptyLowerCaseString, ImmutableList<NonEmptyLowerCaseString>> Get();
    }
}
namespace Next.WTR.Common.Web.Infrastructure.Security.Interface
{
    using System.Collections.Immutable;
    using Next.WTR.Types;

    public interface IAccessResolver
    {
        bool CanAccess(NonEmptyLowerCaseString resourceWithAction, bool isAuthenticated, ImmutableList<NonEmptyLowerCaseString> roles);
    }
}
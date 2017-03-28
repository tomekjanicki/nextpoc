namespace Next.WTR.Common.Web.Infrastructure.Security
{
    using System.Collections.Immutable;
    using System.Linq;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Types;

    public sealed class AccessResolver : IAccessResolver
    {
        private readonly IAccessConfigurationMapProvider _accessConfigurationMapProvider;

        public AccessResolver(IAccessConfigurationMapProvider accessConfigurationMapProvider)
        {
            _accessConfigurationMapProvider = accessConfigurationMapProvider;
        }

        public bool CanAccess(NonEmptyLowerCaseString resourceWithAction, bool isAuthenticated, ImmutableList<NonEmptyLowerCaseString> roles)
        {
            // todo rethink case related to [AllowAnonymous]
            var accessConfigurationMap = _accessConfigurationMapProvider.Get();

            // not on accessConfigurationMap - any role, must be authenticated
            if (!accessConfigurationMap.ContainsKey(resourceWithAction))
            {
                return isAuthenticated;
            }

            var configuredRoles = accessConfigurationMap[resourceWithAction];

            // present on accessConfigurationMap, but no roles - should be anonymous or authenticated
            if (configuredRoles.Count == 0)
            {
                return true;
            }

            // present on accessConfigurationMap with roles - only this role, must be authenticated
            return isAuthenticated && roles.Any(role => configuredRoles.Contains(role));
        }
    }
}
namespace Next.WTR.Web.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Logic;
    using Next.WTR.Types;

    public sealed class AccessConfigurationMapProvider : IAccessConfigurationMapProvider
    {
        private static readonly ImmutableDictionary<NonEmptyLowerCaseString, ImmutableList<NonEmptyLowerCaseString>> Dictionary;

        static AccessConfigurationMapProvider()
        {
            var anonymous = new List<NonEmptyLowerCaseString>().ToImmutableList();

            var buyerOnly = new List<NonEmptyLowerCaseString> { (NonEmptyLowerCaseString)Constants.Roles.Buyer }.ToImmutableList();

            Dictionary = new Dictionary<NonEmptyLowerCaseString, ImmutableList<NonEmptyLowerCaseString>>
            {
                { (NonEmptyLowerCaseString)"version/get", anonymous },
                { (NonEmptyLowerCaseString)"account/login(next.wtr.web.dtos.apis.account.login.data data)", anonymous },
                { (NonEmptyLowerCaseString)"products/insert(next.wtr.web.dtos.apis.product.insert.product product)", buyerOnly },
                { (NonEmptyLowerCaseString)"products/update(system.int32 id, next.wtr.web.dtos.apis.product.update.product product)", buyerOnly },
                { (NonEmptyLowerCaseString)"products/delete(system.int32 id, system.string version)", buyerOnly }
            }.ToImmutableDictionary();
        }

        public ImmutableDictionary<NonEmptyLowerCaseString, ImmutableList<NonEmptyLowerCaseString>> Get()
        {
            return Dictionary;
        }
    }
}

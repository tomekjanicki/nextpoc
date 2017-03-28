namespace Next.WTR.Logic.CQ.Product.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Types;

    public static class Columns
    {
        public static ImmutableDictionary<NonEmptyString, NonEmptyString> GetMappings()
        {
            return new Dictionary<NonEmptyString, NonEmptyString>
            {
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Id), (NonEmptyString)"ID" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Code), (NonEmptyString)"CODE" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Name), (NonEmptyString)"NAME" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Price), (NonEmptyString)"PRICE" }
            }.ToImmutableDictionary();
        }

        public static ImmutableList<NonEmptyString> GetAllowedColumns()
        {
            return GetMappings().Keys.ToImmutableList();
        }
    }
}

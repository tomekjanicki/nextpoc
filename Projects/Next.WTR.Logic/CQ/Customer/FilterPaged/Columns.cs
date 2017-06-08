namespace Next.WTR.Logic.CQ.Customer.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Types;
    using Web.Dtos.Apis.Customer.FilterPaged;

    public static class Columns
    {
        public static ImmutableDictionary<NonEmptyString, NonEmptyString> GetMappings()
        {
            return new Dictionary<NonEmptyString, NonEmptyString>
            {
                { (NonEmptyString)nameof(ResponseCustomer.Id), (NonEmptyString)"ID" },
                { (NonEmptyString)nameof(ResponseCustomer.Code), (NonEmptyString)"CODE" },
                { (NonEmptyString)nameof(ResponseCustomer.Name), (NonEmptyString)"NAME" },
                { (NonEmptyString)nameof(ResponseCustomer.Price), (NonEmptyString)"PRICE" }
            }.ToImmutableDictionary();
        }

        public static ImmutableList<NonEmptyString> GetAllowedColumns()
        {
            return GetMappings().Keys.ToImmutableList();
        }
    }
}

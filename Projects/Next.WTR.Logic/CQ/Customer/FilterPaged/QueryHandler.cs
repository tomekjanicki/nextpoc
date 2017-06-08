namespace Next.WTR.Logic.CQ.Customer.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.Database;
    using Common.Handlers.Interfaces;
    using Common.ValueObjects;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, Paged<Customer>>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION VERSIONINT, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, 1 CANDELETE FROM DBO.PRODUCTS {0} {1}";
        private const string CountQuery = @"SELECT COUNT(*) FROM DBO.PRODUCTS {0}";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Paged<Customer> Handle(Query query)
        {
            var whereFragment = GetWhereFragment(query.Code, query.Name);
            var pagedFragment = CommandHelper.GetPagedFragment(query.OrderByTopSkip.TopSkip, GetSortColumns(query.OrderByTopSkip.OrderByCollection));
            var countQuery = string.Format(CountQuery, whereFragment.Where);
            var selectQuery = string.Format(SelectQuery, whereFragment.Where, pagedFragment.Data);
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var count = (NonNegativeInt)connection.QuerySingle<int>(countQuery, whereFragment.Parameters);
                whereFragment.Parameters.AddDynamicParams(pagedFragment.Parameters);
                var select = connection.Query<Customer>(selectQuery, whereFragment.Parameters).ToImmutableList();
                return Paged<Customer>.Create(count, select);
            }
        }

        private static CommandHelper.WhereResult GetWhereFragment(string code, string name)
        {
            var dp = new DynamicParameters();
            var criteria = new List<NonEmptyString>();
            var codeResult = NonEmptyString.TryCreate(code, (NonEmptyString)"Value");
            var nameResult = NonEmptyString.TryCreate(name, (NonEmptyString)"Value");

            if (codeResult.IsSuccess)
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Customer.Code), (NonEmptyString)nameof(Customer.Code), codeResult.Value));
            }

            if (nameResult.IsSuccess)
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Customer.Name), (NonEmptyString)nameof(Customer.Name), nameResult.Value));
            }

            return CommandHelper.GetWhereStringWithParams(criteria.ToImmutableList(), dp);
        }

        private static NonEmptyString GetSortColumns(OrderByCollection modelOrderByCollection)
        {
            var defaultDatabaseOrderByCollection = Extensions.GetValue(() => NonEmptyOrderByCollection.TryCreate(new List<OrderBy> { OrderBy.Create((NonEmptyString)"CODE", true) }.ToImmutableList()));
            return CommandHelper.GetTranslatedSort(modelOrderByCollection, defaultDatabaseOrderByCollection, Columns.GetMappings());
        }
    }
}

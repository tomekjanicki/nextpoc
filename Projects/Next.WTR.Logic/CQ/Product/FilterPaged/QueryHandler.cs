namespace Next.WTR.Logic.CQ.Product.FilterPaged
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Dapper;
    using Next.WTR.Common.Database;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.ValueObjects;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, Paged<Product>>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION VERSIONINT, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, 1 CANDELETE FROM DBO.PRODUCTS {0} {1}";
        private const string CountQuery = @"SELECT COUNT(*) FROM DBO.PRODUCTS {0}";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Paged<Product> Handle(Query query)
        {
            var whereFragment = GetWhereFragment(query.Code, query.Name);
            var pagedFragment = CommandHelper.GetPagedFragment(query.OrderByTopSkip.TopSkip, GetSortColumns(query.OrderByTopSkip.OrderByCollection));
            var countQuery = string.Format(CountQuery, whereFragment.Where);
            var selectQuery = string.Format(SelectQuery, whereFragment.Where, pagedFragment.Data);
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var count = (NonNegativeInt)connection.QuerySingle<int>(countQuery, whereFragment.Parameters);
                whereFragment.Parameters.AddDynamicParams(pagedFragment.Parameters);
                var select = connection.Query<Product>(selectQuery, whereFragment.Parameters).ToImmutableList();
                return Paged<Product>.Create(count, select);
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
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Product.Code), (NonEmptyString)nameof(Product.Code), codeResult.Value));
            }

            if (nameResult.IsSuccess)
            {
                CommandHelper.SetValues(criteria, dp, CommandHelper.GetLikeCaluse((NonEmptyString)nameof(Product.Name), (NonEmptyString)nameof(Product.Name), nameResult.Value));
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

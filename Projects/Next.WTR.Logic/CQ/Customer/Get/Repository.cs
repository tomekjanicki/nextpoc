namespace Next.WTR.Logic.CQ.Customer.Get
{
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using Dapper;
    using Database.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Repository : IGetRepository<Customer>
    {
        private const string SelectQuery = @"SELECT ID, CODE, NAME, PRICE, VERSION VERSIONINT, CASE WHEN ID < 20 THEN GETDATE() ELSE NULL END DATE, 1 CANDELETE FROM DBO.PRODUCTS WHERE ID = @ID";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<Customer> Get(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingleOrDefault<Customer>(SelectQuery, new { id = id.Value });
            }
        }
    }
}

namespace Next.WTR.Logic.CQ.Product.Delete
{
    using Dapper;
    using Next.WTR.Logic.CQ.Product.Delete.Interfaces;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;
        private readonly SharedQueries _sharedQueries;

        public Repository(IDbConnectionProvider dbConnectionProvider, SharedQueries sharedQueries)
        {
            _dbConnectionProvider = dbConnectionProvider;
            _sharedQueries = sharedQueries;
        }

        public bool ExistsById(PositiveInt id)
        {
            return _sharedQueries.ExistsById(id);
        }

        public Maybe<NonEmptyString> GetRowVersionById(PositiveInt id)
        {
            return _sharedQueries.GetRowVersionById(id);
        }

        public bool CanBeDeleted(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingle<bool>("x", new { id = id.Value });
            }
        }

        public void Delete(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM DBO.PRODUCTS WHERE ID = @ID", new { id = id.Value });
            }
        }
    }
}

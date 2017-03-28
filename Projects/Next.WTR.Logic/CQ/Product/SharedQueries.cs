namespace Next.WTR.Logic.CQ.Product
{
    using System;
    using Dapper;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class SharedQueries
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public SharedQueries(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool ExistsById(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingle<bool>("x", new { id.Value });
            }
        }

        public Maybe<NonEmptyString> GetRowVersionById(PositiveInt id)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var result = connection.QuerySingleOrDefault<byte[]>("SELECT VERSION FROM DBO.PRODUCTS WHERE ID = @ID", new { id = id.Value });
                return result != null ? (NonEmptyString)Convert.ToBase64String(result) : null;
            }
        }
    }
}

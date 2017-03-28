namespace Next.WTR.Logic.CQ.Product.Update
{
    using Dapper;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Repository : IUpdateRepository<Command>
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

        public void Update(Command command)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("UPDATE DBO.PRODUCTS WHERE ID = @ID", new { command.IdVersion.Id.Value });
            }
        }
    }
}

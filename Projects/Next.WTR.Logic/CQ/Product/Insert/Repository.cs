namespace Next.WTR.Logic.CQ.Product.Insert
{
    using Dapper;
    using Database.Interfaces;
    using Interfaces;
    using Types;
    using ValueObjects;

    public sealed class Repository : IRepository
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public bool CodeExists(Code code)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingle<bool>("SELECT CAST(COUNT(*) AS BIT) FROM DBO.PRODUCTS WHERE CODE = @code;", new { code = code.Value });
            }
        }

        public PositiveInt Insert(Command command)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return (PositiveInt)connection.QuerySingle<int>(@"INSERT INTO DBO.PRODUCTS (CODE, NAME, PRICE) VALUES (@code, @name, @price) SELECT SCOPE_IDENTITY()", new { code = command.Code.Value, name = command.Name.Value, price = command.Price.Value });
            }
        }
    }
}

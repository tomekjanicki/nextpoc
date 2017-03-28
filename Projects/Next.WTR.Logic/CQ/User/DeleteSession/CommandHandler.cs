namespace Next.WTR.Logic.CQ.User.DeleteSession
{
    using Dapper;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Logic.Database.Interfaces;

    public sealed class CommandHandler : IVoidRequestHandler<Command>
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public CommandHandler(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public void Handle(Command message)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM dbo.user_session WHERE guid = @sessionId", new { sessionId = message.SessionId.ToString() });
            }
        }
    }
}

namespace Next.WTR.Logic.CQ.User.CreateSession
{
    using System;
    using Dapper;
    using Database.Interfaces;
    using Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        private const string GetAttributesSql = @"
            SELECT
                CAST(CASE WHEN allow_access = 'Y' THEN 0 ELSE 1 END AS BIT) Locked,
                [Password],
                RowId UserNumber,
                user_id AdUserName
            FROM 
                dbo.users
            WHERE 
                user_name = @userId";

        private const string UpdateUserSql = @"
            UPDATE 
                dbo.users
            SET  
                login_date = @loginDate,
                login_time = @loginTime
            WHERE 
                RowID = @rowId";

        private const string DeleteSessionsSql = @"
            DELETE 
                us
            FROM 
                dbo.user_session us
            INNER JOIN 
                dbo.users u
	        ON 
                u.user_id = us.prod_sys_user_id
            WHERE 
                u.RowID = @rowId
                AND 
                system_id = 'PPM'";

        private const string InsertSessionSql = @"
            IF NOT EXISTS 
            (  
                SELECT 
                    TOP 1 guid
                FROM 
                    dbo.user_session 
                WHERE 
                    guid = @sessionId
            )
            BEGIN
                INSERT INTO dbo.user_session
                (system_id, prod_sys_user_id, domain_name, guid, create_date)
                VALUES
                ('PPM', @adUserName, 'NEXT-PLC', @sessionId, @lastSeen)
            END";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<AttributesDto> GetAttributes(NonEmptyString userId)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingleOrDefault<AttributesDto>(GetAttributesSql, new { userId = userId.Value });
            }
        }

        public void SaveLastAttempt(PositiveInt userNumber, DateTime lastAttempt)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                var loginDate = GetDateString(lastAttempt);
                var loginTime = GetTimeString(lastAttempt);
                connection.Execute(UpdateUserSql, new { rowId = userNumber.Value, loginDate, loginTime });
            }
        }

        public void SaveWithNewSession(PositiveInt userNumber, Guid sessionId, DateTime lastSeen, DateTime lastAttempt, NonEmptyString adUserName)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    connection.Execute(DeleteSessionsSql, new { rowId = userNumber.Value }, transaction);
                    connection.Execute(InsertSessionSql, new { sessionId = sessionId.ToString(), adUserName = adUserName.Value, lastSeen }, transaction);
                    var loginDate = GetDateString(lastAttempt);
                    var loginTime = GetTimeString(lastAttempt);
                    connection.Execute(UpdateUserSql, new { rowId = userNumber.Value, loginDate, loginTime }, transaction);
                    transaction.Commit();
                }
            }
        }

        private static string GetDateString(DateTime dateTime)
        {
            const string format = "D2";
            return $"{dateTime.Day.ToString(format)}/{dateTime.Month.ToString(format)}/{dateTime.Year}";
        }

        private static string GetTimeString(DateTime dateTime)
        {
            const string format = "D2";
            return $"{dateTime.Hour.ToString(format)}:{dateTime.Minute.ToString(format)}";
        }
    }
}
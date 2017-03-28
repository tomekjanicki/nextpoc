namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using Dapper;
    using Next.WTR.Logic.CQ.User.GetData.Interfaces;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Repository : IRepository
    {
        private const string GetSessionDataBySessionIdSql = @"
            SELECT 
	            us.create_date LastSeen,
                us.prod_sys_user_id UserName,
                u.RowID UserId
            FROM 
                dbo.user_session us
            INNER JOIN 
                dbo.users u
	        ON 
                u.user_id = us.prod_sys_user_id
            WHERE 
                us.guid = @sessionId";

        private const string SaveLastSeenSql = @"
            UPDATE
                dbo.user_session
            SET
                create_date = @lastSeen
            WHERE
                guid = @sessionId
        ";

        private const string GetGroupsByUserIdSql = @"
            select 
	            g.user_group_name 
            from 
	            dbo.ppm_user_group_links gl
            inner join
	            dbo.ppm_user_groups g
            on
	            gl.user_group_urn = g.user_group_urn
            where 
	            gl.user_number = @userId";

        private readonly IDbConnectionProvider _dbConnectionProvider;

        public Repository(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        public Maybe<AttributesDto> GetSessionDataBySessionId(Guid sessionId)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.QuerySingleOrDefault<AttributesDto>(GetSessionDataBySessionIdSql, new { sessionId = sessionId.ToString() });
            }
        }

        public void SaveLastSeen(Guid sessionId, DateTime lastSeen)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                connection.Execute(SaveLastSeenSql, new { sessionId = sessionId.ToString(), lastSeen });
            }
        }

        public ImmutableList<NonEmptyString> GetGroupsByUserId(NonNegativeInt userId)
        {
            using (var connection = _dbConnectionProvider.GetOpenDbConnection())
            {
                return connection.Query<string>(GetGroupsByUserIdSql, new { userId = userId.Value }).Select(s => (NonEmptyString)s).ToImmutableList();
            }
        }
    }
}
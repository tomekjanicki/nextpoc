namespace Next.WTR.Logic.Database
{
    using System.Data;
    using Next.WTR.Common.Database;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Types;

    public sealed class DbConnectionProvider : IDbConnectionProvider
    {
        public IDbConnection GetOpenDbConnection()
        {
            return DatabaseExtension.GetOpenConnection((NonEmptyString)"Main");
        }
    }
}
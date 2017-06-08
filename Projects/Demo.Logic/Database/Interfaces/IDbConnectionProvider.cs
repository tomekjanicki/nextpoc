namespace Next.WTR.Logic.Database.Interfaces
{
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDbConnection GetOpenDbConnection();
    }
}
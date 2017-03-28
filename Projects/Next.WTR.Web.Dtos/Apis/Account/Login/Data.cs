namespace Next.WTR.Web.Dtos.Apis.Account.Login
{
    public class Data
    {
        public Data(string userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public string UserId { get; }

        public string Password { get; }
    }
}

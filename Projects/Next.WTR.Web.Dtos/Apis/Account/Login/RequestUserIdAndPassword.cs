namespace Next.WTR.Web.Dtos.Apis.Account.Login
{
    using Common.Dtos;

    public class RequestUserIdAndPassword
    {
        public RequestUserIdAndPassword(string userId, string password)
        {
            UserId = userId.IfNullReplaceWithEmptyString();
            Password = password.IfNullReplaceWithEmptyString();
        }

        public string UserId { get; }

        public string Password { get; }
    }
}

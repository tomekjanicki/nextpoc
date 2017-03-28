namespace Next.WTR.Common.Security.Interfaces
{
    using Next.WTR.Types;

    public interface IAuthenticationService
    {
        void SignIn(NonEmptyString sessionId);

        void SignOut();
    }
}
namespace Next.WTR.Web.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using Next.WTR.Common.Security.Interfaces;
    using Next.WTR.Types;

    public sealed class AuthenticationService : IAuthenticationService
    {
        public void SignIn(NonEmptyString sessionId)
        {
            var claims = new List<Claim>
            {
                new Claim(Common.Infrastructure.Security.ClaimTypes.SessionId, sessionId)
            };

            var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(claimsIdentity);
        }

        public void SignOut()
        {
            GetAuthenticationManager().SignOut();
        }

        private static IAuthenticationManager GetAuthenticationManager()
        {
            var owinContext = HttpContext.Current.GetOwinContext();
            return owinContext.Authentication;
        }
    }
}

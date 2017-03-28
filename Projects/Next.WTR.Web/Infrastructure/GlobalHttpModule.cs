namespace Next.WTR.Web.Infrastructure
{
    using System;
    using System.Security.Claims;
    using System.Web;
    using log4net;
    using Next.WTR.Common.Log4Net;
    using Next.WTR.Common.Web.Infrastructure.Security;

    public sealed class GlobalHttpModule : IHttpModule
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalHttpModule));

        public void Init(HttpApplication context)
        {
            context.Error += Error;
            context.PostAuthenticateRequest += PostAuthenticateRequest;
        }

        public void Dispose()
        {
        }

        private static void Error(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var exception = application.Server.GetLastError();
            Logger.Error(() => "An unhandled exception has occured", () => exception);
        }

        private static void PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.User = new ClaimsTransformer().Authenticate(string.Empty, ClaimsPrincipal.Current);
        }
    }
}
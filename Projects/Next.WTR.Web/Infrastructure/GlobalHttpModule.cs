namespace Next.WTR.Web.Infrastructure
{
    using System;
    using System.Web;
    using log4net;
    using Next.WTR.Common.Log4Net;

    public sealed class GlobalHttpModule : IHttpModule
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalHttpModule));

        public void Init(HttpApplication context)
        {
            context.Error += Error;
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
    }
}
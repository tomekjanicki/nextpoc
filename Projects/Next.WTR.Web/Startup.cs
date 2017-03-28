namespace Next.WTR.Web
{
    using System.Web.Http;
    using log4net;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Next.WTR.Common.Log4Net;
    using Next.WTR.Web.Infrastructure;
    using Owin;

    public sealed class Startup
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Startup));

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(GlobalHttpModule));
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            var httpConfiguration = new HttpConfiguration();
            RegisterContainer.Execute(httpConfiguration);
            RegisterWebApiRoutes.Execute(httpConfiguration);
            RegisterWebApiMiscs.Execute(httpConfiguration);
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieSecure = CookieSecureOption.Always
            });
            appBuilder.UseWebApi(httpConfiguration);
            Logger.Info(() => "Application started");
        }
    }
}
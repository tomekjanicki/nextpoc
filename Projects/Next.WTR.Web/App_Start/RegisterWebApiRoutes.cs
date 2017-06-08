namespace Next.WTR.Web
{
    using System.Web.Http;

    public static class RegisterWebApiRoutes
    {
        public static void Execute(HttpConfiguration configuration)
        {
            configuration.MapHttpAttributeRoutes();
            configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}

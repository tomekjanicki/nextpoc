namespace Next.WTR.Web.Infrastructure
{
    using System.Web.Http.ExceptionHandling;
    using log4net;
    using Next.WTR.Common.Log4Net;

    public sealed class GlobalWebApiExceptionLogger : ExceptionLogger
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(GlobalWebApiExceptionLogger));

        public override void Log(ExceptionLoggerContext context)
        {
            Logger.Error(() => "An unhandled exception has occured", () => context.Exception);
        }
    }
}
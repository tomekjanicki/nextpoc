namespace Next.WTR.Web.Infrastructure
{
    using System.Reflection;
    using Common.Tools.Interfaces;

    public sealed class EntryAssemblyProvider : IEntryAssemblyProvider
    {
        public Assembly GetAssembly()
        {
            return typeof(Startup).Assembly;
        }
    }
}
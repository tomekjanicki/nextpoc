namespace Next.WTR.Common.Tools.Interfaces
{
    using System.Reflection;

    public interface IEntryAssemblyProvider
    {
        Assembly GetAssembly();
    }
}
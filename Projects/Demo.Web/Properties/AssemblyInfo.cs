using System.Reflection;
using System.Web;
using Demo.Web;

[assembly: AssemblyTitle("Next.WTR.Web")]
[assembly: AssemblyProduct("Next.WTR.Web")]
[assembly: PreApplicationStartMethod(typeof(Startup), nameof(Startup.Start))]

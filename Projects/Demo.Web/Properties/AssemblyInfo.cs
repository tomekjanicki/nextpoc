using System.Reflection;
using System.Web;
using Demo.Web;

[assembly: AssemblyTitle("Demo.Web")]
[assembly: AssemblyProduct("Demo.Web")]
[assembly: PreApplicationStartMethod(typeof(Startup), nameof(Startup.Start))]

using System.Diagnostics;
using System.IO;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace UnilunchService
{
    public class AzureLocalStorageTraceListener : XmlWriterTraceListener
    {
        public AzureLocalStorageTraceListener()
            : base(Path.Combine(LogDirectory.Path, "WCFServiceWebRole1.svclog"))
        {
        }

        public static DirectoryConfiguration LogDirectory
        {
            get {
                var directory = new DirectoryConfiguration
                    {
                        Container = "wad-tracefiles",
                        DirectoryQuotaInMB = 10,
                        Path = RoleEnvironment.GetLocalResource("WCFServiceWebRole1.svclog").RootPath
                    };
                return directory;
            }
        }
    }
}

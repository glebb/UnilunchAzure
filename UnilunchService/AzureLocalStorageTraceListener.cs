using System.Diagnostics;
using System.IO;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace UnilunchService
{
    public class AzureLocalStorageTraceListener : XmlWriterTraceListener
    {
        public AzureLocalStorageTraceListener()
            : base(Path.Combine(GetLogDirectory().Path, "WCFServiceWebRole1.svclog"))
        {
        }

        public static DirectoryConfiguration GetLogDirectory()
        {
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

#region using directives

using System.Diagnostics;
using System.IO;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

#endregion

namespace UnilunchService
{
// ReSharper disable UnusedMember.Global
    public class AzureLocalStorageTraceListener : XmlWriterTraceListener
// ReSharper restore UnusedMember.Global
    {
        public AzureLocalStorageTraceListener()
            : base(Path.Combine(LogDirectory.Path, "WCFServiceWebRole1.svclog"))
        {
        }

        private static DirectoryConfiguration LogDirectory
        {
            get
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
}
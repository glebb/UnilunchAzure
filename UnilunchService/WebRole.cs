#region using directives

using Microsoft.WindowsAzure.ServiceRuntime;

#endregion

namespace UnilunchService
{
// ReSharper disable UnusedMember.Global
    public class WebRole : RoleEntryPoint
// ReSharper restore UnusedMember.Global
    {
// ReSharper disable RedundantOverridenMember
        public override bool OnStart()
        {
            // To enable the AzureLocalStorageTraceListner, uncomment relevent section in the web.config  
            //DiagnosticMonitorConfiguration diagnosticConfig = DiagnosticMonitor.GetDefaultInitialConfiguration();
            //diagnosticConfig.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);
            //diagnosticConfig.Directories.DataSources.Add(AzureLocalStorageTraceListener.GetLogDirectory());

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

// ReSharper restore RedundantOverridenMember
    }
}
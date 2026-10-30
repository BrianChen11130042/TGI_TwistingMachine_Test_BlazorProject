using CommonRazorLibraryB.Manager.ModbusTcp.Master;
using CommonRazorLibraryB.Tools.NLogWritter;
using Microsoft.Extensions.DependencyInjection;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public ModbusTcpMasterManager modbusTcpMasterManager { get; set; }

        void _createManager()
        {
            modbusTcpMasterManager = provider.GetRequiredService<ModbusTcpMasterManager>();
        }

        void _initManager()
        {
            string logTcp = string.Empty;

            if(!modbusTcpMasterManager.Connect(out logTcp))
            {
                observerLibrary.NotifyNLog(ENLogLevel.Error, logTcp);
            }
        }
    }
}

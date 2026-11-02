using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Manager.ModbusTcp.Master;
using TGI_TwistingMachine_Test_BlazorProject.Scope;

namespace TGI_TwistingMachine_Test_BlazorProject.Service.MachineService
{
    public partial class MachineService
    {
        readonly TgiScope scope;

        public MachineService(TgiScope scope)
        {
            this.scope = scope;

            scope.connectApp.dgSysInitResult += handleSysInitResult;
            scope.connectApp.dgSysDisconnect += handleSysDisconnect;
        }

        public event dgInitResult dgInitResult;
        public event dgDisconnect dgDisconnect;
        public event dgRefreshConfigPage dgRefreshConfigPage;
    }

    public delegate Task dgInitResult(bool success, string msg);
    public delegate Task dgDisconnect(string msg);
    public delegate Task dgRefreshConfigPage();

    public partial class MachineService : IMachineService
    {
        public async Task<List<ModbusTcpMasterConfig>> GetModbusTcpConfig()
        {
            return await scope.connectApp.GetModbusTcpConfig();
        }

        public async Task SetModbusTcpConfig(ModbusTcpMasterConfig config)
        {
            await scope.connectApp.SetModbusTcpConfig(config);

            dgRefreshConfigPage?.Invoke();
        }

        public async Task<List<TgiTwistConfig>> GetTwistConfig()
        {
            return await scope.connectApp.GetTwistConfig();
        }

        public async Task SetTwistConfig(TgiTwistConfig config)
        {
            await scope.connectApp.SetTwistConfig(config);

            dgRefreshConfigPage?.Invoke();
        }

        public async Task Init()
        {
            
        }
    }

    public partial class MachineService
    {
        public async Task handleSysInitResult(bool success, string msg)
        {
            dgInitResult?.Invoke(success, msg);
        }

        public async Task handleSysDisconnect(string msg)
        {
            dgDisconnect?.Invoke(msg);
        }
    }
}

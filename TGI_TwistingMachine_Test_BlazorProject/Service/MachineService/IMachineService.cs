using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Manager.ModbusTcp.Master;

namespace TGI_TwistingMachine_Test_BlazorProject.Service.MachineService
{
    public interface IMachineService
    {
        //Modbus Tcp Config
        Task<List<ModbusTcpMasterConfig>> GetModbusTcpConfig();

        Task SetModbusTcpConfig(ModbusTcpMasterConfig config);


        //Twist Config
        Task<List<TgiTwistConfig>> GetTwistConfig();

        Task SetTwistConfig(TgiTwistConfig config);


        //Systen
        Task Init();

        event dgInitResult dgInitResult;

        event dgDisconnect dgDisconnect;

        event dgRefreshConfigPage dgRefreshConfigPage;
    }
}

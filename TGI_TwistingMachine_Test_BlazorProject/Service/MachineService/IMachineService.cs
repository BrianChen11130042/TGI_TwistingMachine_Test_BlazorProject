using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Manager.ModbusTcp.Master;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;

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

        //Twist Table & Ports Config
        Task<List<TwistingMachineTable>> GetTwistAndPortsTableConfig();

        Task SetTwistAndPortsTableConfig(List<TwistingMachineTable> listData);


        //Systen
        Task Init();

        event dgInitResult dgInitResult;

        event dgDisconnect dgDisconnect;

        event dgConfigResult dgConfigResult;

        event dgRefreshConfigPage dgRefreshConfigPage;
    }
}

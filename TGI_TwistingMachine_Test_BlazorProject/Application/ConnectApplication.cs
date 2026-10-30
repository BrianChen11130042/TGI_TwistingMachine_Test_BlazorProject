using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Manager.ModbusTcp.Master;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Application
{
    public partial class ConnectApplication
    {
        ModbusTcpMasterManager modbusTcpManager;
        TgiTwistConfigManager<ETwist> twistConfigManager;

        public ConnectApplication(ModbusTcpMasterManager modbusTcpManager,
                                  TgiTwistConfigManager<ETwist> twistConfigManager)
        {
            this.modbusTcpManager = modbusTcpManager;
            this.twistConfigManager = twistConfigManager;
        }
    }

    public partial class ConnectApplication
    {
        public async Task<List<ModbusTcpMasterConfig>> GetModbusTcpConfig()
        {
            List<ModbusTcpMasterConfig> list = new List<ModbusTcpMasterConfig>();

            foreach(string dev in Enum.GetNames(typeof(EModbusTcpMaster)))
            {
                ModbusTcpMasterConfig config = modbusTcpManager.Get(dev);

                if(config is not null)
                {
                    list.Add(config);
                }
            }

            return list;
        }

        public async Task SetModbusTcpConfig(ModbusTcpMasterConfig config)
        {
            modbusTcpManager.Set(config.device, config);
            modbusTcpManager.Save();
        }

        public async Task<List<TgiTwistConfig>> GetTwistConfig()
        {
            List<TgiTwistConfig> list = new List<TgiTwistConfig>();

            foreach(string dev in Enum.GetNames(typeof(ETwist)))
            {
                TgiTwistConfig config = twistConfigManager.Get(dev);

                if(config is not null)
                {
                    list.Add(config);
                }
            }

            return list;
        }

        public async Task SetTwistConfig(TgiTwistConfig config)
        {
            twistConfigManager.Set(config.device, config);
            twistConfigManager.Save();
        }
    }
}

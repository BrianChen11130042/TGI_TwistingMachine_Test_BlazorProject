using TGI_TwistingMachine_Test_BlazorProject.Application;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public ConnectApplication connectApp { get; set; }

        void _createApplication()
        {
            connectApp = new ConnectApplication(modbusTcpMasterManager, twistConfig);
        }

        void _initApplication()
        {
            observerLibrary.AddSystemObserver(connectApp);
        }
    }
}

using TGI_TwistingMachine_Test_BlazorProject.Application;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public ConnectApplication connectApp { get; set; }

        public TwistTableApplication twistTableApp { get; set; }

        public TwistMissionApplication twistMissionApp { get; set; }

        void _createApplication()
        {
            connectApp = new ConnectApplication(modbusTcpMasterManager, twistConfig);
            twistTableApp = new TwistTableApplication(twistTableLibrary, observerLibrary);
            twistMissionApp = new TwistMissionApplication(twistLibrary, 
                                                          twistLibrary,
                                                          twistTableLibrary,
                                                          logsTableLibrary,
                                                          _getDcTwistMissionDataLibrary());
        }

        Dictionary<ETwist, ITwistMissionDataLibrary> _getDcTwistMissionDataLibrary()
        {
            Dictionary<ETwist, ITwistMissionDataLibrary> dc = new Dictionary<ETwist, ITwistMissionDataLibrary>();

            foreach (KeyValuePair<ETwist, TwistMissionDataLibrary> pair in dcTwistMissionDataLibrary)
            {
                dc.Add(pair.Key, pair.Value);
            }

            return dc;
        }

        void _initApplication()
        {
            observerLibrary.AddSystemObserver(connectApp);
            observerLibrary.AddLogObserver(twistMissionApp);
            observerLibrary.AddTwistInformObserver(twistMissionApp);

            twistMissionApp.InitDcTwistMachineNumber().GetAwaiter().GetResult();
        }
    }
}

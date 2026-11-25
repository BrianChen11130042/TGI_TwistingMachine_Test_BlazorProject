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
                                                          dcTwistMissionDataLibrary);
        }

        Dictionary<ETwist, ITwistMissionDataLibrary> dcTwistMissionDataLibrary
        {
            get
            {
                return new Dictionary<ETwist, ITwistMissionDataLibrary>()
                {
                    { ETwist.Twist1, twist1MissionDataLibrary },
                    { ETwist.Twist2, twist2MissionDataLibrary },
                    { ETwist.Twist3, twist3MissionDataLibrary },
                    { ETwist.Twist4, twist4MissionDataLibrary },
                    { ETwist.Twist5, twist5MissionDataLibrary },
                };
            }
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

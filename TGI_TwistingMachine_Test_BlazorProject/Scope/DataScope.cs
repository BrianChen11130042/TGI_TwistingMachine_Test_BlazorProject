using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Error;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Initial;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public TwistTableLibrary twistTableLibrary { get; set; }
        public LogsTableLibrary logsTableLibrary { get; set; }

        public InitialDataLibrary initialDataLibrary { get; set; }
        public ErrorDataLibrary errorDataLibrary { get; set; }

        public TwistRegularDataLibrary allTwistRegularDataLibrary { get; set; }

        public TwistMissionDataLibrary twist1MissionDataLibrary { get; set; }
        public TwistMissionDataLibrary twist2MissionDataLibrary { get; set; }
        public TwistMissionDataLibrary twist3MissionDataLibrary { get; set; }
        public TwistMissionDataLibrary twist4MissionDataLibrary { get; set; }
        public TwistMissionDataLibrary twist5MissionDataLibrary { get; set; }


        void _createDataLibrary()
        {
            twistTableLibrary = provider.GetRequiredService<TwistTableLibrary>();
            logsTableLibrary = provider.GetRequiredService<LogsTableLibrary>();

            initialDataLibrary = new InitialDataLibrary(logsTableLibrary,
                                                        twistTableLibrary,
                                                        observerLibrary,
                                                        observerLibrary,
                                                        observerLibrary,
                                                        observerLibrary);

            errorDataLibrary = new ErrorDataLibrary(logsTableLibrary,
                                                    observerLibrary,
                                                    observerLibrary,
                                                    observerLibrary);

            allTwistRegularDataLibrary = new TwistRegularDataLibrary(logsTableLibrary,
                                                                     twistTableLibrary,
                                                                     observerLibrary,
                                                                     observerLibrary);

            twist1MissionDataLibrary = new TwistMissionDataLibrary(logsTableLibrary,
                                                                   twistTableLibrary,
                                                                   observerLibrary);

            twist2MissionDataLibrary = new TwistMissionDataLibrary(logsTableLibrary,
                                                                   twistTableLibrary,
                                                                   observerLibrary);

            twist3MissionDataLibrary = new TwistMissionDataLibrary(logsTableLibrary,
                                                                   twistTableLibrary,
                                                                   observerLibrary);

            twist4MissionDataLibrary = new TwistMissionDataLibrary(logsTableLibrary,
                                                                   twistTableLibrary,
                                                                   observerLibrary);

            twist5MissionDataLibrary = new TwistMissionDataLibrary(logsTableLibrary,
                                                                   twistTableLibrary,
                                                                   observerLibrary);
        }
    }
}

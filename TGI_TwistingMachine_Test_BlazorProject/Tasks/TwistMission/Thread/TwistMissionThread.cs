using CommonRazorLibraryB.Base.FiniteStateMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission.Thread
{

    public partial class TwistMissionThread
    {
        readonly List<TwistMissionTask> listTwistMission;

        public TwistMissionThread(List<TwistMissionTask> listTwistMission)
        {
            this.listTwistMission = listTwistMission;

            interval = 1;
        }
    }

    public enum ETwistMissionThread
    {
        None,
        TwistMission
    }

    public partial class TwistMissionThread : FSMBase<ETwistMissionThread, int>
    {
        public async override Task Init()
        {
            foreach (TwistMissionTask mission in listTwistMission)
            {
                mission.Set(ES1.Action, ETwistMission.CheckMission, 0);
            }

            Set(ES1.Action, ETwistMissionThread.TwistMission, 0);
        }

        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case ETwistMissionThread.None:
                    Set(ES1.Finish, ETwistMissionThread.None, 0);
                    break;

                case ETwistMissionThread.TwistMission:
                    switch(S3)
                    {
                        case 0:
                            await Task.WhenAll(listTwistMission.Select(t => t.Run()));

                            if (listTwistMission.Any(t => t.key == EHandshakeKey.Finish && t.isError))
                            {
                                SaveState();
                                isError = true;
                            }

                            Set(0);

                            break;
                    }
                    break;
            }
        }

        public async override Task Error()
        {
            //throw new NotImplementedException();
        }

        public async override Task Finish()
        {
            //throw new NotImplementedException();
        }

        public async override Task Idle()
        {
            //throw new NotImplementedException();
        }
    }
}

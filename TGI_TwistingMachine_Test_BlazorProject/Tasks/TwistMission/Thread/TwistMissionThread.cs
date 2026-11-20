using CommonRazorLibraryB.Base.FiniteStateMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission.Thread
{

    public partial class TwistMissionThread
    {
        readonly TwistMissionTask twist1;
        readonly TwistMissionTask twist2;
        readonly TwistMissionTask twist3;
        readonly TwistMissionTask twist4;
        readonly TwistMissionTask twist5;


        public TwistMissionThread(TwistMissionTask twist1,
                                  TwistMissionTask twist2,
                                  TwistMissionTask twist3,
                                  TwistMissionTask twist4,
                                  TwistMissionTask twist5)
        {
            this.twist1 = twist1;
            this.twist2 = twist2;
            this.twist3 = twist3;
            this.twist4 = twist4;
            this.twist5 = twist5;

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
            twist1.Set(ES1.Action, ETwistMission.CheckMission, 0);
            twist2.Set(ES1.Action, ETwistMission.CheckMission, 0);
            twist3.Set(ES1.Action, ETwistMission.CheckMission, 0);
            twist4.Set(ES1.Action, ETwistMission.CheckMission, 0);
            twist5.Set(ES1.Action, ETwistMission.CheckMission, 0);

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
                            await Task.WhenAll(twist1.Run(),
                                               twist2.Run(),
                                               twist3.Run(),
                                               twist4.Run(),
                                               twist5.Run());

                            if ((twist1.key == EHandshakeKey.Finish && twist1.isError) ||
                                (twist2.key == EHandshakeKey.Finish && twist2.isError) ||
                                (twist3.key == EHandshakeKey.Finish && twist3.isError) ||
                                (twist4.key == EHandshakeKey.Finish && twist4.isError) ||
                                (twist5.key == EHandshakeKey.Finish && twist5.isError))
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

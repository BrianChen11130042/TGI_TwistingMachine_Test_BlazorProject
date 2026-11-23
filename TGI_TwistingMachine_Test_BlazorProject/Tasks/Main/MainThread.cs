using CommonRazorLibraryB.Base.FiniteStateMachine;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.SystemControl.Thread;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission.Thread;
using TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistRegular.Thread;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.Main
{
    public partial class MainThread
    {
        readonly SystemControlThread controlThread;
        readonly TwistRegularThread twistRegularThread;
        readonly TwistMissionThread twistMissionThread;

        public MainThread(SystemControlThread controlThread,
                          TwistRegularThread twistRegularThread,
                          TwistMissionThread twistMissionThread)
        {
            this.controlThread = controlThread;
            this.twistRegularThread = twistRegularThread;
            this.twistMissionThread = twistMissionThread;

            interval = 1;
        }
    }

    public enum EMainThread
    {
        None,
        Monitor
    }

    public partial class MainThread : FSMBase<EMainThread, int>
    {
        public async override Task Init()
        {
            switch(S3)
            {
                case 0:
                    controlThread.ResetKey();
                    controlThread.Set(ES1.Action, ESystemControlThread.InitialTask, 0);

                    Set(10);
                    break;

                case 10:
                    if(controlThread.key == EHandshakeKey.Finish)
                    {
                        if(controlThread.isError)
                        {
                            Set(ES1.None, EMainThread.None, 0);
                        }
                        else
                        {
                            twistRegularThread.ResetKey();
                            twistMissionThread.ResetKey();

                            twistRegularThread.Set(ES1.Init, ETwistRegularThread.None, 0);
                            twistMissionThread.Set(ES1.Init, ETwistMissionThread.None, 0);

                            Set(ES1.Action, EMainThread.Monitor, 0);
                        }
                    }
                    else
                    {
                        Set(10);
                    }
                    break;
            }
        }

        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case EMainThread.None:
                    Set(ES1.Finish, EMainThread.None, 0);
                    break;

                case EMainThread.Monitor:
                    switch(S3)
                    {
                        case 0:
                            if(twistRegularThread.isError)
                            {
                                SaveState();
                                Set(ES1.Error, EMainThread.None, 0);
                            }
                            else
                            {
                                Set(10);
                            }
                            break;

                        case 10:
                            if(twistMissionThread.isError)
                            {
                                SaveState();
                                Set(ES1.Error, EMainThread.None, 0);
                            }
                            else
                            {
                                Set(0);
                            }
                            break;
                    }
                    break;
            }
        }

        public async override Task Error()
        {
            switch(S3)
            {
                case 0:
                    controlThread.ResetKey();
                    controlThread.Set(ES1.Action, ESystemControlThread.ErrorTask, 0);

                    Set(10);
                    break;

                case 10:
                    if(controlThread.key == EHandshakeKey.Finish)
                    {
                        Set(ES1.Idle, EMainThread.None, 0);
                    }
                    else
                    {
                        Set(10);
                    }
                    break;
            }
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

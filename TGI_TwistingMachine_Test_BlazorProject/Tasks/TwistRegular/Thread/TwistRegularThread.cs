using CommonRazorLibraryB.Base.FiniteStateMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistRegular.Thread
{
    public partial class TwistRegularThread
    {
        readonly TwistRegularTask twistRegularTask;

        public TwistRegularThread(TwistRegularTask twistRegularTask)
        {
            this.twistRegularTask = twistRegularTask;

            interval = 1;
        }
    }

    public enum ETwistRegularThread
    {
        None,
        TwistRegular
    }

    public partial class TwistRegularThread : FSMBase<ETwistRegularThread, int>
    {
        public async override Task Init()
        {
            twistRegularTask.Set(ES1.Action, ETwistRegular.MonitorStatus, 0);

            Set(ES1.Action, ETwistRegularThread.TwistRegular, 0);
        }

        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case ETwistRegularThread.None:
                    Set(ES1.Finish, ETwistRegularThread.None, 0);
                    break;

                case ETwistRegularThread.TwistRegular:
                    switch(S3)
                    {
                        case 0:
                            await twistRegularTask.Run();

                            if(twistRegularTask.key == EHandshakeKey.Finish && twistRegularTask.isError)
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

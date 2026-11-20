using CommonRazorLibraryB.Base.FiniteStateMachine;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Initial;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.SystemControl
{

    public partial class InitialTask : IInitialTaskPack
    {
        readonly IInitialTaskPack pack;

        public InitialTask(IInitialTaskPack pack)
        {
            this.pack = pack;

            interval = 1;
        }

        public Task<bool> InitSystem()
        {
            return pack.InitSystem();
        }

        public Task NotifyInitialFail()
        {
            return pack.NotifyInitialFail();
        }

        public Task NotifyInitialSuccess()
        {
            return pack.NotifyInitialSuccess();
        }

        public Task NotifyLogUpdate()
        {
            return pack.NotifyLogUpdate();
        }

        public Task NotifyTwistUpdate()
        {
            return pack.NotifyTwistUpdate();
        }
    }

    public enum EInitialTask
    {
        None,
        StartInit
    }

    public partial class InitialTask : FSMBase<EInitialTask, int>
    {
        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case EInitialTask.None:
                    Set(ES1.Finish, EInitialTask.None, 0);
                    break;

                case EInitialTask.StartInit:
                    switch(S3)
                    {
                        case 0:
                            if(await InitSystem())
                            {
                                await NotifyTwistUpdate();
                                await NotifyLogUpdate();
                                await NotifyInitialSuccess();

                                Set(ES1.Finish, EInitialTask.None, 0);
                            }
                            else
                            {
                                await NotifyInitialFail();

                                Set(ES1.Error, EInitialTask.None, 0);
                            }
                            break;
                    }
                    break;
            }
        }

        public async override Task Error()
        {
            isError = true;
            key = EHandshakeKey.Finish;
            Set(ES1.Idle, EInitialTask.None, 0);
        }

        public async override Task Finish()
        {
            isError = false;
            key = EHandshakeKey.Finish;
            Set(ES1.Idle, EInitialTask.None, 0);
        }

        public async override Task Idle()
        {
            //throw new NotImplementedException();
        }

        public async override Task Init()
        {
            //throw new NotImplementedException();
        }
    }
}

using CommonRazorLibraryB.Base.FiniteStateMachine;
using DevExpress.Utils.Filtering.Internal;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistRegular;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistRegular
{
    public partial class TwistRegularTask : ITwistRegularTaskPack
    {
        readonly ITwistRegularTaskPack pack;

        public TwistRegularTask(ITwistRegularTaskPack pack)
        {
            this.pack = pack;
            interval = 1;
        }

        public Task<bool> GetTwistStatus()
        {
            return pack.GetTwistStatus();
        }

        public Task<bool> UpsertTwistTable()
        {
            return pack.UpsertTwistTable();
        }

        public Task NotifyTwistUpdate()
        {
            return pack.NotifyTwistUpdate();
        }
    }

    public enum ETwistRegular
    {
        None,
        MonitorStatus
    }

    public partial class TwistRegularTask : FSMBase<ETwistRegular, int>
    {
        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case ETwistRegular.None:
                    Set(ES1.Finish, ETwistRegular.None, 0);
                    break;

                case ETwistRegular.MonitorStatus:
                    switch(S3)
                    {
                        case 0:
                            if(await GetTwistStatus())
                            {
                                if(await UpsertTwistTable())
                                {
                                    await NotifyTwistUpdate();

                                    Set(ETwistRegular.MonitorStatus, 0);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistRegular.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistRegular.None, 0);
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
            Set(ES1.Idle, ETwistRegular.None, 0);
        }

        public async override Task Finish()
        {
            isError = false;
            key = EHandshakeKey.Finish;
            Set(ES1.Idle, ETwistRegular.None, 0);
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

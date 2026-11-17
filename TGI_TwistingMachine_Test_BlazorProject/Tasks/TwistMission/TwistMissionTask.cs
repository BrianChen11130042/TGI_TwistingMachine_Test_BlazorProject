using CommonRazorLibraryB.Base.FiniteStateMachine;
using TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistMission;

namespace TGI_TwistingMachine_Test_BlazorProject.Tasks.TwistMission
{
    public partial class TwistMissionTask : ITwistMissionTaskPack
    {
        readonly ITwistMissionTaskPack pack;

        public TwistMissionTask(ITwistMissionTaskPack pack)
        {
            this.pack = pack;
            interval = 1;
        }

        public Task GetMachineNumber()
        {
            return pack.GetMachineNumber();
        }

        public bool IsGetTwistTableInQueue()
        {
            return pack.IsGetTwistTableInQueue();
        }

        public bool IsAutoModeSwitch()
        {
            return pack.IsAutoModeSwitch();
        }

        public bool IsAmrLoadSilkCake()
        {
            return pack.IsAmrLoadSilkCake();
        }

        public bool IsPersonnelLoadEmptyBobbin()
        {
            return pack.IsPersonnelLoadEmptyBobbin();
        }

        public bool IsPersonnelCutAndCollect()
        {
            return pack.IsPersonnelCutAndCollect();
        }

        public bool IsAmrUnloadSilkBobbin()
        {
            return pack.IsAmrUnloadSilkBobbin();
        }

        public bool IsAmrUnloadEmptyCake()
        {
            return pack.IsAmrUnloadEmptyCake();
        }

        public bool IsPersonnelClean()
        {
            return pack.IsPersonnelClean();
        }
    }

    public enum ETwistMission
    {
        None,
        CheckMission,

        AutoModeSwitch,

        AmrLoadSilkCake,
        PersonnelLoadEmptyBobbin,
        PersonnelCutAndCollect,
        AmrUnloadSilkBobbin,
        AmrUnloadEmptyCake,
        PersonnelClean
    }

    public partial class TwistMissionTask : FSMBase<ETwistMission, int>
    {
        public async override Task Action()
        {
            key = EHandshakeKey.Run;

            switch(S2)
            {
                case ETwistMission.None:
                    Set(ES1.Finish, ETwistMission.None, 0);
                    break;

                case ETwistMission.CheckMission:

                    await GetMachineNumber();

                    if(!IsGetTwistTableInQueue())
                    {
                        Set(ETwistMission.CheckMission, 0);
                        break;
                    }

                    if (IsAutoModeSwitch())
                    {
                        Set(ETwistMission.AutoModeSwitch, 0);
                    }
                    else if(IsAmrLoadSilkCake())
                    {
                        Set(ETwistMission.AmrLoadSilkCake, 0);
                    }
                    else if(IsPersonnelLoadEmptyBobbin())
                    {
                        Set(ETwistMission.PersonnelLoadEmptyBobbin, 0);
                    }
                    else if(IsPersonnelCutAndCollect())
                    {
                        Set(ETwistMission.PersonnelCutAndCollect, 0);
                    }
                    else if(IsAmrUnloadSilkBobbin())
                    {
                        Set(ETwistMission.AmrUnloadSilkBobbin, 0);
                    }
                    else if(IsAmrUnloadEmptyCake())
                    {
                        Set(ETwistMission.AmrUnloadEmptyCake, 0);
                    }
                    else if(IsPersonnelClean())
                    {
                        Set(ETwistMission.PersonnelClean, 0);
                    }
                    else
                    {
                        Set(ETwistMission.CheckMission, 0);
                    }
                    break;
            }
        }

        public async override Task Error()
        {
            isError = true;
            key = EHandshakeKey.Finish;
            Set(ES1.Idle, ETwistMission.None, 0);
        }

        public async override Task Finish()
        {
            isError = false;
            key = EHandshakeKey.Finish;
            Set(ES1.Idle, ETwistMission.None, 0);
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

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

        #region Method

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

        public bool IsPersonnelLoadNoSilkBobbin()
        {
            return pack.IsPersonnelLoadNoSilkBobbin();
        }

        public bool IsPersonnelCutAndCollect()
        {
            return pack.IsPersonnelCutAndCollect();
        }

        public bool IsAmrUnloadSilkBobbin()
        {
            return pack.IsAmrUnloadSilkBobbin();
        }

        public bool IsAmrUnloadNoSilkCake()
        {
            return pack.IsAmrUnloadNoSilkCake();
        }

        public bool IsPersonnelCleanMachine()
        {
            return pack.IsPersonnelCleanMachine();
        }

        public Task<bool> SetAutoModeSwitch()
        {
            return pack.SetAutoModeSwitch();
        }

        public Task<bool> AddLogAutoModeSwitch()
        {
            return pack.AddLogAutoModeSwitch();
        }

        public void ResetCmdAutoMode()
        {
            pack.ResetCmdAutoMode();
        }

        public Task<bool> AddAmrLoadSilkCakeAssignTable()
        {
            return pack.AddAmrLoadSilkCakeAssignTable();
        }

        public Task<bool> AddLogAmrLoadSilkCakeStart()
        {
            return pack.AddLogAmrLoadSilkCakeStart();
        }

        public Task<bool> MonitorAmrLoadSilkCakeAssignTable()
        {
            return pack.MonitorAmrLoadSilkCakeAssignTable();
        }

        public bool IsAmrLoadSilkCakeAssignTableFinish()
        {
            return pack.IsAmrLoadSilkCakeAssignTableFinish();
        }

        public Task<bool> SetAmrLoadSilkCakeComplete()
        {
            return pack.SetAmrLoadSilkCakeComplete();
        }

        public Task<bool> AddLogAmrLoadSilkCakeFinish()
        {
            return pack.AddLogAmrLoadSilkCakeFinish();
        }

        public void ResetCmdMissionCompleted()
        {
            pack.ResetCmdMissionCompleted();
        }

        public Task<bool> AddLogPersonnelLoadNoSilkBobbinStart()
        {
            return pack.AddLogPersonnelLoadNoSilkBobbinStart();
        }

        public bool IsPersonnelLoadNoSilkBobbinFinish()
        {
            return pack.IsPersonnelLoadNoSilkBobbinFinish();
        }

        public Task<bool> SetBobbinTableNoSilkBobbin()
        {
            return pack.SetBobbinTableNoSilkBobbin();
        }

        public Task<bool> SetPersonnelLoadNoSilkBobbinComplete()
        {
            return pack.SetPersonnelLoadNoSilkBobbinComplete();
        }

        public Task<bool> AddLogPersonnelLoadNoSilkBobbinFinish()
        {
            return pack.AddLogPersonnelLoadNoSilkBobbinFinish();
        }

        public Task<bool> AddLogPersonnelCutAndCollectStart()
        {
            return pack.AddLogPersonnelCutAndCollectStart();
        }

        public Task<bool> TransferSilkFromCakeToBobbinTable()
        {
            return pack.TransferSilkFromCakeToBobbinTable();
        }

        public bool IsPersonnelCutAndCollectFinish()
        {
            return pack.IsPersonnelCutAndCollectFinish();
        }

        public Task<bool> SetPersonnelCutAndCollectComplete()
        {
            return pack.SetPersonnelCutAndCollectComplete();
        }

        public Task<bool> AddLogPersonnelCutAndCollectFinish()
        {
            return pack.AddLogPersonnelCutAndCollectFinish();
        }

        public Task<bool> AddAmrUnloadSilkBobbinAssignTable()
        {
            return pack.AddAmrUnloadSilkBobbinAssignTable();
        }

        public Task<bool> AddLogAmrUnloadSilkBobbinStart()
        {
            return pack.AddLogAmrUnloadSilkBobbinStart();
        }

        public Task<bool> MonitorAmrUnloadSilkBobbinAssignTable()
        {
            return pack.MonitorAmrUnloadSilkBobbinAssignTable();
        }

        public bool IsAmrUnloadSilkBobbinAssignTableFinish()
        {
            return pack.IsAmrUnloadSilkBobbinAssignTableFinish();
        }

        public Task<bool> SetAmrUnloadSilkBobbinComplete()
        {
            return pack.SetAmrUnloadSilkBobbinComplete();
        }

        public Task<bool> AddLogAmrUnloadSilkBobbinFinish()
        {
            return pack.AddLogAmrUnloadSilkBobbinFinish();
        }

        public Task<bool> AddAmrUnloadNoSilkCakeAssignTable()
        {
            return pack.AddAmrUnloadNoSilkCakeAssignTable();
        }

        public Task<bool> AddLogAmrUnloadNoSilkCakeStart()
        {
            return pack.AddLogAmrUnloadNoSilkCakeStart();
        }

        public Task<bool> MonitorAmrUnloadNoSilkCakeAssignTable()
        {
            return pack.MonitorAmrUnloadNoSilkCakeAssignTable();
        }

        public bool IsAmrUnloadNoSilkCakeAssignTableFinish()
        {
            return pack.IsAmrUnloadNoSilkCakeAssignTableFinish();
        }

        public Task<bool> SetAmrUnloadNoSilkCakeComplete()
        {
            return pack.SetAmrUnloadNoSilkCakeComplete();
        }

        public Task<bool> AddLogAmrUnloadNoSilkCakeFinish()
        {
            return pack.AddLogAmrUnloadNoSilkCakeFinish();
        }

        public Task<bool> AddLogPersonnelCleanMachineStart()
        {
            return pack.AddLogPersonnelCleanMachineStart();
        }

        public bool IsPersonnelCleanMachineFinish()
        {
            return pack.IsPersonnelCleanMachineFinish();
        }

        public Task<bool> SetPersonnelCleanMachineComplete()
        {
            return pack.SetPersonnelCleanMachineComplete();
        }

        public Task<bool> AddLogPersonnelCleanMachineFinish()
        {
            return pack.AddLogPersonnelCleanMachineFinish();
        }

        #endregion
    }

    public enum ETwistMission
    {
        None,
        CheckMission,

        AutoModeSwitch,

        AmrLoadSilkCake,
        PersonnelLoadNoSilkBobbin,
        PersonnelCutAndCollect,
        AmrUnloadSilkBobbin,
        AmrUnloadNoSilkCake,
        PersonnelCleanMachine
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
                    switch(S3)
                    {
                        case 0:
                            await GetMachineNumber();

                            if (!IsGetTwistTableInQueue())
                            {
                                Set(ETwistMission.CheckMission, 0);
                                break;
                            }

                            if (IsAutoModeSwitch())
                            {
                                Set(ETwistMission.AutoModeSwitch, 0);
                            }
                            else if (IsAmrLoadSilkCake())
                            {
                                Set(ETwistMission.AmrLoadSilkCake, 0);
                            }
                            else if (IsPersonnelLoadNoSilkBobbin())
                            {
                                Set(ETwistMission.PersonnelLoadNoSilkBobbin, 0);
                            }
                            else if (IsPersonnelCutAndCollect())
                            {
                                Set(ETwistMission.PersonnelCutAndCollect, 0);
                            }
                            else if (IsAmrUnloadSilkBobbin())
                            {
                                Set(ETwistMission.AmrUnloadSilkBobbin, 0);
                            }
                            else if (IsAmrUnloadNoSilkCake())
                            {
                                Set(ETwistMission.AmrUnloadNoSilkCake, 0);
                            }
                            else if (IsPersonnelCleanMachine())
                            {
                                Set(ETwistMission.PersonnelCleanMachine, 0);
                            }
                            else
                            {
                                Set(ETwistMission.CheckMission, 0);
                            }
                            break;
                    }
                    break;

                case ETwistMission.AutoModeSwitch:
                    switch(S3)
                    {
                        case 0:
                            if(await SetAutoModeSwitch())
                            {
                                if(await AddLogAutoModeSwitch())
                                {
                                    ResetCmdAutoMode();

                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;
                    }
                    break;

                case ETwistMission.AmrLoadSilkCake:
                    switch(S3)
                    {
                        case 0:
                            if(await AddAmrLoadSilkCakeAssignTable())
                            {
                                if(await AddLogAmrLoadSilkCakeStart())
                                {
                                    Set(10);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(await MonitorAmrLoadSilkCakeAssignTable())
                            {
                                if(IsAmrLoadSilkCakeAssignTableFinish())
                                {
                                    Set(20);
                                }
                                else
                                {
                                    Set(10);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 20:
                            if(await SetAmrLoadSilkCakeComplete())
                            {
                                if (await AddLogAmrLoadSilkCakeFinish())
                                {
                                    ResetCmdMissionCompleted();

                                    Set(30);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 30:
                            if(IsGetTwistTableInQueue())
                            {
                                if(!IsAmrLoadSilkCake())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(30);
                                }
                            }
                            else
                            {
                                Set(30);
                            }
                            break;
                    }
                    break;

                case ETwistMission.PersonnelLoadNoSilkBobbin:
                    switch(S3)
                    {
                        case 0:
                            if(await AddLogPersonnelLoadNoSilkBobbinStart())
                            {
                                Set(10);
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(IsPersonnelLoadNoSilkBobbinFinish())
                            {
                                if(await SetBobbinTableNoSilkBobbin())
                                {
                                    Set(20);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                Set(10);
                            }
                            break;

                        case 20:
                            if(await SetPersonnelLoadNoSilkBobbinComplete())
                            {
                                if(await AddLogPersonnelLoadNoSilkBobbinFinish())
                                {
                                    ResetCmdMissionCompleted();

                                    Set(30);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 30:
                            if (IsGetTwistTableInQueue())
                            {
                                if (!IsPersonnelLoadNoSilkBobbin())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(30);
                                }
                            }
                            else
                            {
                                Set(30);
                            }
                            break;
                    }
                    break;

                case ETwistMission.PersonnelCutAndCollect:
                    switch(S3)
                    {
                        case 0:
                            if(await AddLogPersonnelCutAndCollectStart())
                            {
                                if(await TransferSilkFromCakeToBobbinTable())
                                {
                                    Set(10);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(IsPersonnelCutAndCollectFinish())
                            {
                                if(await SetPersonnelCutAndCollectComplete())
                                {
                                    if(await AddLogPersonnelCutAndCollectFinish())
                                    {
                                        ResetCmdMissionCompleted();

                                        Set(20);
                                    }
                                    else
                                    {
                                        SaveState();
                                        Set(ES1.Error, ETwistMission.None, 0);
                                    }
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                Set(10);
                            }
                            break;

                        case 20:
                            if (IsGetTwistTableInQueue())
                            {
                                if (!IsPersonnelCutAndCollect())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(20);
                                }
                            }
                            else
                            {
                                Set(20);
                            }
                            break;
                    }
                    break;

                case ETwistMission.AmrUnloadSilkBobbin:
                    switch(S3)
                    {
                        case 0:
                            if(await AddAmrUnloadSilkBobbinAssignTable())
                            {
                                if(await AddLogAmrUnloadSilkBobbinStart())
                                {
                                    Set(10);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(await MonitorAmrUnloadSilkBobbinAssignTable())
                            {
                                if(IsAmrUnloadSilkBobbinAssignTableFinish())
                                {
                                    Set(20);
                                }
                                else
                                {
                                    Set(10);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 20:
                            if (await SetAmrUnloadSilkBobbinComplete())
                            {
                                if(await AddLogAmrUnloadSilkBobbinFinish())
                                {
                                    ResetCmdMissionCompleted();

                                    Set(30);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 30:
                            if (IsGetTwistTableInQueue())
                            {
                                if (!IsAmrUnloadSilkBobbin())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(30);
                                }
                            }
                            else
                            {
                                Set(30);
                            }
                            break;
                    }
                    break;

                case ETwistMission.AmrUnloadNoSilkCake:
                    switch(S3)
                    {
                        case 0:
                            if(await AddAmrUnloadNoSilkCakeAssignTable())
                            {
                                if(await AddLogAmrUnloadNoSilkCakeStart())
                                {
                                    Set(10);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(await MonitorAmrUnloadNoSilkCakeAssignTable())
                            {
                                if(IsAmrUnloadNoSilkCakeAssignTableFinish())
                                {
                                    Set(20);
                                }
                                else
                                {
                                    Set(10);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 20:
                            if(await SetAmrUnloadNoSilkCakeComplete())
                            {
                                if(await AddLogAmrUnloadNoSilkCakeFinish())
                                {
                                    ResetCmdMissionCompleted();

                                    Set(30);
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 30:
                            if (IsGetTwistTableInQueue())
                            {
                                if (!IsAmrUnloadNoSilkCake())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(30);
                                }
                            }
                            else
                            {
                                Set(30);
                            }
                            break;
                    }
                    break;

                case ETwistMission.PersonnelCleanMachine:
                    switch(S3)
                    {
                        case 0:
                            if(await AddLogPersonnelCleanMachineStart())
                            {
                                Set(10);
                            }
                            else
                            {
                                SaveState();
                                Set(ES1.Error, ETwistMission.None, 0);
                            }
                            break;

                        case 10:
                            if(IsPersonnelCleanMachineFinish())
                            {
                                if(await SetPersonnelCleanMachineComplete())
                                {
                                    if(await AddLogPersonnelCleanMachineFinish())
                                    {
                                        ResetCmdMissionCompleted();

                                        Set(20);
                                    }
                                    else
                                    {
                                        SaveState();
                                        Set(ES1.Error, ETwistMission.None, 0);
                                    }
                                }
                                else
                                {
                                    SaveState();
                                    Set(ES1.Error, ETwistMission.None, 0);
                                }
                            }
                            else
                            {
                                Set(10);
                            }
                            break;

                        case 20:
                            if (IsGetTwistTableInQueue())
                            {
                                if (!IsPersonnelCleanMachine())
                                {
                                    Set(ETwistMission.CheckMission, 0);
                                }
                                else
                                {
                                    Set(20);
                                }
                            }
                            else
                            {
                                Set(20);
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

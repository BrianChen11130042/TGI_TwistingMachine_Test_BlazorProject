using CommonRazorLibraryB.Library.TGITwistingMachine.Adapter;
using CommonRazorLibraryB.Library.TGITwistingMachine.Package;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission;

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistMission
{

    public partial class TwistMissionTaskPack<ETwist>
    {
        readonly ETwist twist;

        readonly ITgiTwistPackage<ETwist> ITwistPack;
        readonly ITgiTwistAdapter<ETwist> ITwistOp;

        readonly ITwistMissionDataLibrary IDataLib;

        public TwistMissionTaskPack(ETwist twist,
                                    ITgiTwistPackage<ETwist> ITwistPack,
                                    ITgiTwistAdapter<ETwist> ITwistOp,
                                    ITwistMissionDataLibrary IDataLib)
        {
            this.twist = twist;

            this.ITwistPack = ITwistPack;
            this.ITwistOp = ITwistOp;

            this.IDataLib = IDataLib;
        }

        const string info = "Inform";
        const string err = "Error";

        const int amrLoadSilkCake = 1 << 0;           // 1,   Bit 0 通知/完成AMR上Silk Cake
        const int personnelLoadEmptyBobbin = 1 << 1;  // 2,   Bit 1 通知/完成人工Empty Bobbin與繞線
        const int twisting = 1 << 2;                  // 4,   Bit 2 通知/完成撚紗
        const int personnelCutAndCollect = 1 << 3;    // 8,   Bit 3 通知/完成人工剪線收線
        const int amrUnloadSilkBobbin = 1 << 4;       // 16,  Bit 4 通知/完成AMR下Silk Bobbin
        const int amrUnloadEmptyCake = 1 << 5;        // 32,  Bit 5 通知/完成AMR下Empty Cake
        const int personnelCleanMachine = 1 << 6;     // 64,  Bit 6 通知/完成人工清設備
    }

    public partial class TwistMissionTaskPack<ETwist> : ITwistMissionTaskPack
    {
        public async Task GetMachineNumber()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                await ITwistOp.GetMachineNumber(twist);

                IDataLib.MachineNumber = ITwistPack.Packages[twist].property.machineNumber;
            }
            finally
            {
                ITwistPack.Packages[twist].gate.Release();
            }
        }

        public bool IsGetTwistTableInQueue()
        {
            return IDataLib.GetTwistTableInQueue();
        }

        public bool IsAutoModeSwitch()
        {
            if (IDataLib.CmdAutoMode != 0 && IDataLib.CmdAutoMode != 1)
            {
                IDataLib.CmdAutoMode = 99;
                return false;
            }

            if (IDataLib.CmdAutoMode != IDataLib.TwistTable.AutoMode)
            {
                return true;
            }
            else
            {
                IDataLib.CmdAutoMode = 99;
                return false;
            }
        }

        public bool IsAmrLoadSilkCake()
        {
            if(IDataLib.TwistTable.MissionStartedInform == amrLoadSilkCake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPersonnelLoadEmptyBobbin()
        {
            if(IDataLib.TwistTable.MissionStartedInform == personnelLoadEmptyBobbin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPersonnelCutAndCollect()
        {
            if(IDataLib.TwistTable.MissionStartedInform == personnelCutAndCollect)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAmrUnloadSilkBobbin()
        {
            if(IDataLib.TwistTable.MissionStartedInform == amrUnloadSilkBobbin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAmrUnloadEmptyCake()
        {
            if(IDataLib.TwistTable.MissionStartedInform == amrUnloadEmptyCake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPersonnelClean()
        {
            if(IDataLib.TwistTable.MissionStartedInform == personnelCleanMachine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> SetAutoModeSwitch()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.autoMode = (ushort)IDataLib.CmdAutoMode;

                if (await ITwistOp.SetAutoMode(twist))
                {
                    return true;
                }
                else
                {
                    string nlog = ITwistPack.Packages[twist].errorLog;
                    await IDataLib.WriteNLogError(nlog);
                    return false;
                }
            }
            finally
            {
                ITwistPack.Packages[twist].gate.Release();
            }
        }

        public async Task<bool> AddLogAutoModeSwitch()
        {
            string temp = $"Set Auto/Manual Mode {IDataLib.CmdAutoMode} Success";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddAmrLoadSilkCakeAssignTable()
        {
            //目前先暫訂這個想法 => 新增AmrLoadSilkCakeAssignTable後由AmrLoadSilkCakeAssignTask負責機台cake port以及車子撮合叫車任務 && cake資料搬運(amr port -> twist cake port)

            return true;
        }

        public async Task<bool> AddLogAmrLoadSilkCakeStart()
        {
            string temp = $"AmrLoadSilkCake {amrLoadSilkCake} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public async Task<bool> MonitorAmrLoadSilkCakeAssignTable()
        {
            //抓取剛剛新增的AmrLoadSilkCakeAssignTable
            return true;
        }

        public bool IsAmrLoadSilkCakeAssignTableFinish()
        {
            //看AmrLoadSilkCakeAssignTable是否完成

            if(IDataLib.CmdMissionCompleted == amrLoadSilkCake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetAmrLoadSilkCakeComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = amrLoadSilkCake;

                if (await ITwistOp.SetTaskCompletion(twist))
                {
                    return true;
                }
                else
                {
                    string nlog = ITwistPack.Packages[twist].errorLog;
                    await IDataLib.WriteNLogError(nlog);
                    return false;
                }
            }
            finally
            {
                ITwistPack.Packages[twist].gate.Release();
            }
        }

        public async Task<bool> AddLogAmrLoadSilkCakeFinish()
        {
            string temp = $"AmrLoadSilkCake {amrLoadSilkCake} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public void ResetCmdAutoMode()
        {
            IDataLib.CmdAutoMode = 99;
        }

        public void ResetCmdMissionCompleted()
        {
            IDataLib.CmdMissionCompleted = 0;
        }
    }
}

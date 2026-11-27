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
        const int personnelLoadNoSilkBobbin = 1 << 1; // 2,   Bit 1 通知/完成人工上NoSilk Bobbin與繞線
        const int twisting = 1 << 2;                  // 4,   Bit 2 通知/完成撚紗
        const int personnelCutAndCollect = 1 << 3;    // 8,   Bit 3 通知/完成人工剪線收線
        const int amrUnloadSilkBobbin = 1 << 4;       // 16,  Bit 4 通知/完成AMR下Silk Bobbin
        const int amrUnloadNoSilkCake = 1 << 5;       // 32,  Bit 5 通知/完成AMR下NoSilk Cake
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

        public bool IsAmrLoadSilkCake()
        {
            if (IDataLib.TwistTable.MissionStartedInform != amrLoadSilkCake)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == amrLoadSilkCake)
                return false;

            return true;
        }

        public bool IsPersonnelLoadNoSilkBobbin()
        {
            if (IDataLib.TwistTable.MissionStartedInform != personnelLoadNoSilkBobbin)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == personnelLoadNoSilkBobbin)
                return false;

            return true;
        }

        public bool IsPersonnelCutAndCollect()
        {
            if (IDataLib.TwistTable.MissionStartedInform != personnelCutAndCollect)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == personnelCutAndCollect)
                return false;

            return true;
        }

        public bool IsAmrUnloadSilkBobbin()
        {
            if (IDataLib.TwistTable.MissionStartedInform != amrUnloadSilkBobbin)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == amrUnloadSilkBobbin)
                return false;

            return true;
        }

        public bool IsAmrUnloadNoSilkCake()
        {
            if (IDataLib.TwistTable.MissionStartedInform != amrUnloadNoSilkCake)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == amrUnloadNoSilkCake)
                return false;

            return true;
        }

        public bool IsPersonnelCleanMachine()
        {
            if (IDataLib.TwistTable.MissionStartedInform != personnelCleanMachine)
                return false;

            if (IDataLib.TwistTable.MissionCompletedInform == personnelCleanMachine)
                return false;

            return true;
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddAmrLoadSilkCakeAssignTable()
        {
            //目前先暫訂這個想法 => 新增AmrLoadSilkCakeAssignTable後由AmrLoadSilkCakeAssignTask負責機台cake port以及cake車子撮合叫車任務 && cake資料搬運(cake amr port -> twist cake port)

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
        public async Task<bool> AddLogPersonnelLoadNoSilkBobbinStart()
        {
            string temp = $"PersonnelLoadNoSilkBobbin {personnelLoadNoSilkBobbin} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public bool IsPersonnelLoadNoSilkBobbinFinish()
        {
            if (IDataLib.CmdMissionCompleted == personnelLoadNoSilkBobbin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetBobbinTableNoSilkBobbin()
        {
            //在bobbin table 所有port更改為無絲bobbin狀態 然後寫入資料庫和InQueue

            return true;
        }

        public async Task<bool> SetPersonnelLoadNoSilkBobbinComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = personnelLoadNoSilkBobbin;

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

        public async Task<bool> AddLogPersonnelLoadNoSilkBobbinFinish()
        {
            string temp = $"PersonnelLoadNoSilkBobbin {personnelLoadNoSilkBobbin} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddLogPersonnelCutAndCollectStart()
        {
            string temp = $"PersonnelCutAndCollect {personnelCutAndCollect} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public async Task<bool> TransferSilkFromCakeToBobbinTable()
        {
            //cake table 絲綢資料轉移到 bobbin table, cake所有port更改為無絲cake狀態,bobbin所有port更改為有絲bobbin狀態, 然後寫入資料庫和InQueue

            return true;
        }

        public bool IsPersonnelCutAndCollectFinish()
        {
            if (IDataLib.CmdMissionCompleted == personnelCutAndCollect)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetPersonnelCutAndCollectComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = personnelCutAndCollect;

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

        public async Task<bool> AddLogPersonnelCutAndCollectFinish()
        {
            string temp = $"PersonnelCutAndCollect {personnelCutAndCollect} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddAmrUnloadSilkBobbinAssignTable()
        {
            //目前先暫訂這個想法 => 新增AmrUnloadSilkBobbinAssignTable後由AmrUnloadSilkBobbinAssignTask負責機台bobbin port以及bobbin車子撮合叫車任務 && bobbin資料搬運(twist bobbin port -> bobbin amr port)

            return true;
        }

        public async Task<bool> AddLogAmrUnloadSilkBobbinStart()
        {
            string temp = $"AmrUnloadSilkBobbin {amrUnloadSilkBobbin} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public async Task<bool> MonitorAmrUnloadSilkBobbinAssignTable()
        {
            //抓取剛剛新增的AmrUnloadSilkBobbinAssignTable
            return true;
        }

        public bool IsAmrUnloadSilkBobbinAssignTableFinish()
        {
            //看AmrUnloadSilkBobbinAssignTable是否完成

            if (IDataLib.CmdMissionCompleted == amrUnloadSilkBobbin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetAmrUnloadSilkBobbinComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = amrUnloadSilkBobbin;

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

        public async Task<bool> AddLogAmrUnloadSilkBobbinFinish()
        {
            string temp = $"AmrUnloadSilkBobbin {amrUnloadSilkBobbin} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddAmrUnloadNoSilkCakeAssignTable()
        {
            //目前先暫訂這個想法 => 新增AmrUnloadNoSilkCakeAssignTable後由AmrUnloadNoSilkCakeAssignTask負責機台cake port以及cake車子撮合叫車任務 && cake資料搬運(twist cake port -> cake amr port)

            return true;
        }

        public async Task<bool> AddLogAmrUnloadNoSilkCakeStart()
        {
            string temp = $"AmrUnloadNoSilkCake {amrUnloadNoSilkCake} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public async Task<bool> MonitorAmrUnloadNoSilkCakeAssignTable()
        {
            //抓取剛剛新增的AmrUnloadNoSilkCakeAssignTable
            return true;
        }

        public bool IsAmrUnloadNoSilkCakeAssignTableFinish()
        {
            //看AmrUnloadNoSilkCakeAssignTable是否完成

            if (IDataLib.CmdMissionCompleted == amrUnloadNoSilkCake)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetAmrUnloadNoSilkCakeComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = amrUnloadNoSilkCake;

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

        public async Task<bool> AddLogAmrUnloadNoSilkCakeFinish()
        {
            string temp = $"AmrUnloadNoSilkCake {amrUnloadNoSilkCake} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public async Task<bool> AddLogPersonnelCleanMachineStart()
        {
            string temp = $"PersonnelCleanMachine {personnelCleanMachine} Start";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }

        public bool IsPersonnelCleanMachineFinish()
        {
            if (IDataLib.CmdMissionCompleted == personnelCleanMachine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SetPersonnelCleanMachineComplete()
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.taskCompletion = personnelCleanMachine;

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

        public async Task<bool> AddLogPersonnelCleanMachineFinish()
        {
            string temp = $"PersonnelCleanMachine {personnelCleanMachine} Finish";

            return await IDataLib.AddLogTable(twist.ToString(), info, temp);
        }
    }

    public partial class TwistMissionTaskPack<ETwist>
    {
        public void ResetCmdMissionCompleted()
        {
            IDataLib.CmdMissionCompleted = 0;
        }
    }
}

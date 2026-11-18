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

        const int AmrLoadSilkCake = 1 << 0;           // 1,   Bit 0 通知AMR上Silk Cake
        const int PersonnelLoadEmptyBobbin = 1 << 1;  // 2,   Bit 1 通知人工Empty Bobbin與繞線
        const int StartTwisting = 1 << 2;             // 4,   Bit 2 開始撚紗
        const int PersonnelCutAndCollect = 1 << 3;    // 8,   Bit 3 通知人工剪線收線
        const int AmrUnloadSilkBobbin = 1 << 4;       // 16,  Bit 4 通知AMR下Silk Bobbin
        const int AmrUnloadEmptyCake = 1 << 5;        // 32,  Bit 5 通知AMR下Empty Cake
        const int PersonnelCleanMachine = 1 << 6;     // 64,  Bit 6 通知人工清設備
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
            if(IDataLib.TwistTable.MissionStartedInform == AmrLoadSilkCake)
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
            if(IDataLib.TwistTable.MissionStartedInform == PersonnelLoadEmptyBobbin)
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
            if(IDataLib.TwistTable.MissionStartedInform == PersonnelCutAndCollect)
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
            if(IDataLib.TwistTable.MissionStartedInform == AmrUnloadSilkBobbin)
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
            if(IDataLib.TwistTable.MissionStartedInform == AmrUnloadEmptyCake)
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
            if(IDataLib.TwistTable.MissionStartedInform == PersonnelCleanMachine)
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

        public async Task<bool> SetLogAutoModeSwitch()
        {
            string temp = $"Set {IDataLib.CmdAutoMode} Mode Success";

            if(await IDataLib.AddLogTable(twist.ToString(), info, temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ResetAutoModeSwitch()
        {
            IDataLib.CmdAutoMode = 99;
        }
    }
}

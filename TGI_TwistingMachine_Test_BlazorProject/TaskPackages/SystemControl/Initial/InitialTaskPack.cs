using CommonRazorLibraryB.Library.TGITwistingMachine.Adapter;
using CommonRazorLibraryB.Library.TGITwistingMachine.Package;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Initial;

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Initial
{
    public partial class InitialTaskPack<ETwist>
    {
        readonly List<ETwist> listTwist;

        readonly ITgiTwistPackage<ETwist> ITwistPack;
        readonly ITgiTwistAdapter<ETwist> ITwistOp;

        readonly IInitialDataLibrary IDataLib;

        public InitialTaskPack(List<ETwist> listTwist,
                               ITgiTwistPackage<ETwist> ITwistPack,
                               ITgiTwistAdapter<ETwist> ITwistOp,
                               IInitialDataLibrary IDataLib)
        {
            this.listTwist = listTwist;
            this.ITwistPack = ITwistPack;
            this.ITwistOp = ITwistOp;
            this.IDataLib = IDataLib;
        }
    }

    public partial class InitialTaskPack<ETwist> : IInitialTaskPack
    {
        public async Task<bool> InitSystem()
        {
            if (!await _getCheckAllTwistStatus())
                return false;

            if (!await _initTwistTableInQueue())
                return false;

            if(! await _checkTwistTableData())
                return false;

            if(! await _syncTwistTableData())
                return false;

            if (! await _initLogTableInQueue())
                return false;

            return true;
        }

        async Task<bool> _getCheckAllTwistStatus()
        {
            IDataLib.DcTwistStatusDto.Clear();

            foreach (ETwist twist in listTwist)
            {
                await ITwistPack.Packages[twist].gate.WaitAsync();

                try
                {
                    if (await ITwistOp.GetTwistStatus(twist))
                    {
                        await ITwistOp.GetMachineNumber(twist);

                        if(!await _checkTwistData(twist))
                        {
                            return false;
                        }
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

            return true;
        }

        async Task<bool> _checkTwistData(ETwist twist)
        {
            int _plcMachineNumber = ITwistPack.Packages[twist].property.get.machineNumber;
            int _configMachineNumber = ITwistPack.Packages[twist].property.machineNumber;

            if (_plcMachineNumber != _configMachineNumber)
            {
                string nlog = "Config and plc machine number are different";
                await IDataLib.WriteNLogError(nlog);
                return false;
            }

            if (IDataLib.DcTwistStatusDto.ContainsKey(_plcMachineNumber))
            {
                await IDataLib.WriteNLogError("Duplicate machine number");
                return false;
            }

            IDataLib.DcTwistStatusDto.Add(_plcMachineNumber, new TwistStatusDto()
            {
                machineNumber = ITwistPack.Packages[twist].property.get.machineNumber,
                processState = ITwistPack.Packages[twist].property.get.processState,
                machineStatus = ITwistPack.Packages[twist].property.get.machineStatus,
                missionStartedInform = ITwistPack.Packages[twist].property.get.missionStartedInform,
                missionCompletedInform = ITwistPack.Packages[twist].property.get.missionCompletedInform,
                autoMode = ITwistPack.Packages[twist].property.get.isAutoMode
            });

            return true;
        }

        async Task<bool> _initTwistTableInQueue()
        {
            return await IDataLib.InitTwistTableInQueue();
        }

        async Task<bool> _checkTwistTableData()
        {
            return await IDataLib.CheckTwistTableData();
        }

        async Task<bool> _syncTwistTableData()
        {
            return await IDataLib.UpsertTwistTable();
        }

        async Task<bool> _initLogTableInQueue()
        {
            return await IDataLib.InitLogTableInQueue();
        }

        public async Task NotifyInitialSuccess()
        {
            await IDataLib.NotifyInitialResult(true, "Init Success");
        }

        public async Task NotifyInitialFail()
        {
            await IDataLib.NotifyInitialResult(false, "Init Fail");
        }

        public async Task NotifyTwistUpdate()
        {
            await IDataLib.NotifyTwistUpdate();
        }

        public async Task NotifyLogUpdate()
        {
            await IDataLib.NotifyLogUpdate();
        }
    }
}

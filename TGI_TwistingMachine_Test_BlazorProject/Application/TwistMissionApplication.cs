using CommonRazorLibraryB.Library.TGITwistingMachine.Adapter;
using CommonRazorLibraryB.Library.TGITwistingMachine.Package;
using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.Application
{
    public partial class TwistMissionApplication
    {
        readonly ITgiTwistPackage<ETwist> ITwistPack;
        readonly ITgiTwistAdapter<ETwist> ITwistOp;

        readonly ITwistTableLibrary ITwistTableLib;
        readonly ILogsTableLibrary ILogsTableLib;

        readonly Dictionary<ETwist, ITwistMissionDataLibrary> dcTwistMissionDataLib;

        readonly INLogObservable INLogObser;

        public TwistMissionApplication(ITgiTwistPackage<ETwist> ITwistPack,
                                       ITgiTwistAdapter<ETwist> ITwistOp,
                                       ITwistTableLibrary ITwistTableLib,
                                       ILogsTableLibrary ILogsTableLib,
                                       Dictionary<ETwist, ITwistMissionDataLibrary> dcTwistMissionDataLib,
                                       INLogObservable INLogObser)
        {
            this.ITwistPack = ITwistPack;
            this.ITwistOp = ITwistOp;

            this.ITwistTableLib = ITwistTableLib;
            this.ILogsTableLib = ILogsTableLib;

            this.dcTwistMissionDataLib = dcTwistMissionDataLib;

            this.INLogObser = INLogObser;
        }

        public async Task InitDcTwistMachineNumber()
        {
            DcTwistMachineNumber.Clear();

            foreach (ETwist twist in Enum.GetValues(typeof(ETwist)))
            {
                await ITwistPack.Packages[twist].gate.WaitAsync();

                try
                {
                    await ITwistOp.GetMachineNumber(twist);

                    if (!DcTwistMachineNumber.ContainsKey(twist))
                    {
                        DcTwistMachineNumber.Add(twist, ITwistPack.Packages[twist].property.machineNumber);
                    }
                }
                finally
                {
                    ITwistPack.Packages[twist].gate.Release();
                }
            }
        }
    }

    public partial class TwistMissionApplication
    {
        Dictionary<ETwist, int> _dcTwistMachineNumber { get; set; } = new Dictionary<ETwist, int>();

        public Dictionary<ETwist, int> DcTwistMachineNumber
        {
            get
            {
                return _dcTwistMachineNumber;
            }
            set
            {
                _dcTwistMachineNumber = value;
            }
        }
    }

    public partial class TwistMissionApplication
    {
        public TwistingMachineTable GetTwistTableInQueue(ETwist twist)
        {
            return _getTwistTable(ITwistTableLib.ListTwistInQueue, twist);
        }

        public List<LogTable> GetListLogTableInQueue(ETwist twist)
        {
            return ILogsTableLib.listLogDataInQueue.Where(x => x.Equipment == twist.ToString()).ToList();
        }

        public bool SetMissionCompleted(ETwist twist, int completed)
        {
            if (!dcTwistMissionDataLib.TryGetValue(twist, out var dataLib))
                return false;

            dataLib.CmdMissionCompleted = completed;

            return true;
        }

        public async Task<bool> SetAutoManualMode(ETwist twist, int mode)
        {
            if (await _setAutoModeSwitch(twist, mode))
            {
                if(await  _addLogAutoModeSwitch(twist, mode))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        async Task<bool> _setAutoModeSwitch(ETwist twist, int mode)
        {
            await ITwistPack.Packages[twist].gate.WaitAsync();

            try
            {
                ITwistPack.Packages[twist].property.set.autoMode = (ushort)mode;

                if (await ITwistOp.SetAutoMode(twist))
                {
                    return true;
                }
                else
                {
                    string nlog = ITwistPack.Packages[twist].errorLog;
                    await INLogObser.NotifyNLog(ENLogLevel.Error, nlog);
                    return false;
                }
            }
            finally
            {
                ITwistPack.Packages[twist].gate.Release();
            }
        }

        async Task<bool> _addLogAutoModeSwitch(ETwist twist, int mode)
        {
            string temp = $"Set Auto/Manual Mode {mode} Success";

            var result = await ILogsTableLib.AddLogData(_getLogTable(twist.ToString(), "Inform", temp));

            if (result.status)
            {
                return result.status;
            }
            else
            {
                string nlog = result.msg;
                await INLogObser.NotifyNLog(ENLogLevel.Error, nlog);
                return result.status;
            }
        }

        LogTable _getLogTable(string equip, string logType, string msg)
        {
            LogTable table = new LogTable()
            {
                LogType = logType,
                Equipment = equip,
                Msg = msg,
                RecordTime = DateTime.Now,
            };

            return table;
        }
    }

    public delegate Task dgTgiLogsUpdated(List<LogTable> listLog);
    public delegate Task dgTgiDcTwistUpdated(Dictionary<ETwist, TwistingMachineTable> dcTwist);

    public partial class TwistMissionApplication : ILogObserver, ITwistInformObserver
    {
        public dgTgiLogsUpdated dgTgiLogsUpdated;

        public async Task HandleLogUpdated(List<LogTable> list)
        {
            dgTgiLogsUpdated?.Invoke(list);
        }

        public dgTgiDcTwistUpdated dgTgiDcTwistUpdated;

        public async Task HandleTwistUpdated(List<TwistingMachineTable> list)
        {
            dgTgiDcTwistUpdated?.Invoke(_toDcTwistTable(list));
        }
    }

    public partial class TwistMissionApplication
    {
        Dictionary<ETwist, TwistingMachineTable> _toDcTwistTable(List<TwistingMachineTable> list)
        {
            Dictionary<ETwist, TwistingMachineTable> dcTarget = new Dictionary<ETwist, TwistingMachineTable>();

            foreach (ETwist twist in DcTwistMachineNumber.Keys)
            {
                if(!dcTarget.ContainsKey(twist))
                {
                    dcTarget.Add(twist, _getTwistTable(list, twist));
                }
            }

            return dcTarget;
        }

        TwistingMachineTable _getTwistTable(List<TwistingMachineTable> list, ETwist twist)
        {
            if (!DcTwistMachineNumber.TryGetValue(twist, out int machineNumber))
            {
                return new TwistingMachineTable();
            }

            TwistingMachineTable? table = list.FirstOrDefault(x => x.MachineNumber == machineNumber);

            if(table is not null)
            {
                return table;
            }
            else
            {
                return new TwistingMachineTable();
            }
        }
    }
}

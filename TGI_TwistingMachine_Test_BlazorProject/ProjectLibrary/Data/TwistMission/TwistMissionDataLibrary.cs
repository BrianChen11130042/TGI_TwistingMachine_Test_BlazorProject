using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission
{

    public partial class TwistMissionDataLibrary
    {
        readonly ILogsTableLibrary ILogTableLibrary;
        readonly ITwistTableLibrary ITwistTableLibrary;

        readonly INLogObservable INLogObser;

        public TwistMissionDataLibrary(ILogsTableLibrary ILogTableLibrary,
                                       ITwistTableLibrary ITwistTableLibrary,
                                       INLogObservable INLogObser)
        {
            this.ILogTableLibrary = ILogTableLibrary;
            this.ITwistTableLibrary = ITwistTableLibrary;

            this.INLogObser = INLogObser;
        }

        async Task _writeNLogError(string log)
        {
            await INLogObser.NotifyNLog(ENLogLevel.Error, log);
        }

        async Task _writeNLogInform(string log)
        {
            await INLogObser.NotifyNLog(ENLogLevel.Info, log);
        }
    }

    public partial class TwistMissionDataLibrary
    {
        int _machineNumber { get; set; } = 0;

        public int MachineNumber
        {
            get
            {
                return _machineNumber;
            }
            set
            {
                _machineNumber = value;
            }
        }

        // 99 : 復歸 
        // 0 : Manual 
        // 1 : Auto

        int _cmdAutoMode { get; set; } = 99;

        public int CmdAutoMode 
        {
            get
            {
                return _cmdAutoMode;
            }
            set
            {
                _cmdAutoMode = value;
            }
        }

        // 0 : 復歸 
        // 1 : AMR完成上Silk Cake 
        // 2 : 人工完成上NoSilk Bobbin 
        // 4 : 撚紗完成 
        // 8 : 人工完成剪線收線 
        // 16 : Amr完成下Silk Bobbin
        // 32 : Amr完成下NoSilk Cake 
        // 64 : 人工完成清設備

        int _cmdMissionCompleted { get; set; } = 0;

        public int CmdMissionCompleted
        {
            get
            {
                return _cmdMissionCompleted;
            }
            set
            {
                _cmdMissionCompleted = value;
            }
        }

        TwistingMachineTable _twistTable { get; set; } = new TwistingMachineTable();

        public TwistingMachineTable TwistTable
        {
            get
            {
                return _twistTable;
            }
            set
            {
                _twistTable = value;
            }
        }
    }

    public partial class TwistMissionDataLibrary : ITwistMissionDataLibrary
    {
        public bool GetTwistTableInQueue()
        {
            TwistingMachineTable table = ITwistTableLibrary.ListTwistInQueue
                                                           .FirstOrDefault(x => x.MachineNumber == MachineNumber);

            if(table is not null)
            {
                TwistTable = table;
                return true;
            }
            else
            {
                TwistTable = new TwistingMachineTable();
                return false;
            }
        }
    }

    public partial class TwistMissionDataLibrary
    {
        string _Log { get; set; } = string.Empty;

        public async Task<bool> AddLogTable(string equip, string type, string log)
        {
            if (_Log == log)
                return true;

            var result = await ILogTableLibrary.AddLogData(_getLogTable(equip, type, log));

            if(result.status)
            {
                _Log = log;
                return result.status;
            }
            else
            {
                await _writeNLogError(result.msg);
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

    public partial class TwistMissionDataLibrary
    {
        public async Task WriteNLogError(string log)
        {
            await _writeNLogError(log);
        }

        public async Task WriteNLogInform(string log)
        {
            await _writeNLogInform(log);
        }
    }
}

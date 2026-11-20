using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Initial
{
    public partial class InitialDataLibrary
    {
        readonly ILogsTableLibrary ILogsTableLibrary;
        readonly ITwistTableLibrary ITwistTableLibrary;

        readonly ISystemObservable ISysObser;
        readonly ITwistInformObservable ITwistInformObser;
        readonly ILogObservable ILogObser;
        readonly INLogObservable INLogObser;

        public InitialDataLibrary(ILogsTableLibrary ILogsTableLibrary,
                                  ITwistTableLibrary ITwistTableLibrary,
                                  ISystemObservable ISysObser,
                                  ITwistInformObservable ITwistInformObser,
                                  ILogObservable ILogObser,
                                  INLogObservable INLogObser)
        {
            this.ILogsTableLibrary = ILogsTableLibrary;
            this.ITwistTableLibrary = ITwistTableLibrary;
            this.ISysObser = ISysObser;
            this.ITwistInformObser = ITwistInformObser;
            this.ILogObser = ILogObser;
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

    public partial class InitialDataLibrary
    {
        Dictionary<int, TwistStatusDto> _dcTwistStatusDto { get; set; } = new Dictionary<int, TwistStatusDto>();

        public Dictionary<int, TwistStatusDto> DcTwistStatusDto
        {
            get
            {
                return _dcTwistStatusDto;
            }
            set
            {
                _dcTwistStatusDto = value;
            }
        }
    }

    public partial class InitialDataLibrary : IInitialDataLibrary
    {
        public async Task<bool> InitTwistTableInQueue()
        {
            var result = await ITwistTableLibrary.InitListTwistInQueue();

            if(result.status)
            {
                return result.status;
            }
            else
            {
                await WriteNLogError(result.msg);
                return result.status;
            }
        }

        public async Task<bool> CheckTwistTableData()
        {
            foreach (var pair in DcTwistStatusDto)
            {
                bool isExist = ITwistTableLibrary.ListTwistInQueue.Any(x => x.MachineNumber == pair.Key);

                if (!isExist)
                {
                    await WriteNLogError("Database Twist table not found machine data");
                    return false;
                }
            }

            bool hasDuplicate = ITwistTableLibrary.ListTwistInQueue.GroupBy(x => x.MachineNumber)
                                                                   .Any(g => g.Count() > 1);

            if(hasDuplicate)
            {
                await WriteNLogError("Database Twist table has duplicate machine number");
                return false;
            }

            return true;
        }

        public async Task<bool> UpsertTwistTable()
        {
            Dictionary<int, TwistingMachineTable> _dcTwistInQueue = ITwistTableLibrary.ListTwistInQueue
                                                                                      .ToDictionary(x => x.MachineNumber);

            foreach (var pair in DcTwistStatusDto)
            {
                if (!_dcTwistInQueue.TryGetValue(pair.Key, out var table))
                    continue;

                if (_isStatusChanged(pair.Value, table))
                {
                    var target = _buildUpsertTarget(pair.Value, table);

                    var result = await ITwistTableLibrary.UpsertTwistTable(target);

                    if (!result.status)
                    {
                        await _writeNLogError(result.msg);
                        return result.status;
                    }
                }
            }

            return true;
        }

        bool _isStatusChanged(TwistStatusDto dto, TwistingMachineTable table)
        {
            bool isChange = dto.processState != table.ProcessState
                            || dto.machineStatus != table.MachineStatus
                            || dto.missionStartedInform != table.MissionStartedInform
                            || dto.missionCompletedInform != table.MissionCompletedInform
                            || dto.autoMode != table.AutoMode;

            return isChange;
        }

        TwistingMachineTable _buildUpsertTarget(TwistStatusDto dto, TwistingMachineTable table)
        {
            return new TwistingMachineTable
            {
                Id = table.Id,
                MachineNumber = table.MachineNumber,
                ProcessState = dto.processState,
                MachineStatus = dto.machineStatus,
                MissionStartedInform = dto.missionStartedInform,
                MissionCompletedInform = dto.missionCompletedInform,
                AutoMode = dto.autoMode,
                CakeTables = table.CakeTables,
                BobbinTables = table.BobbinTables
            };
        }

        public async Task<bool> InitLogTableInQueue()
        {
            var result = await ILogsTableLibrary.InitListLogInQueue();

            if (result.status)
            {
                return result.status;
            }
            else
            {
                await WriteNLogError(result.msg);
                return result.status;
            }
        }

        public async Task NotifyInitialResult(bool success, string msg)
        {
            await ISysObser.NotifyInitResult(success, msg);
        }

        public async Task NotifyTwistUpdate()
        {
            await ITwistInformObser.NotifyTwistUpdated(ITwistTableLibrary.ListTwistInQueue);
        }

        public async Task NotifyLogUpdate()
        {
            await ILogObser.NotifyLogUpdated(ILogsTableLibrary.listLogDataInQueue);
        }

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

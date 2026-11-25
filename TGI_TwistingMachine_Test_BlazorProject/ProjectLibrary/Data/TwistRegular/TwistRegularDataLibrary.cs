using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistRegular;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data
{
    public partial class TwistRegularDataLibrary
    {
        readonly ILogsTableLibrary ILogTableLibrary;
        readonly ITwistTableLibrary ITwistTableLibrary;

        readonly ITwistInformObservable ITwistInformObser;
        readonly INLogObservable INLogObser;

        public TwistRegularDataLibrary(ILogsTableLibrary ILogTableLibrary,
                                       ITwistTableLibrary ITwistTableLibrary,
                                       ITwistInformObservable ITwistInformObser,
                                       INLogObservable INLogObser)
        {
            this.ILogTableLibrary = ILogTableLibrary;
            this.ITwistTableLibrary = ITwistTableLibrary;

            this.ITwistInformObser = ITwistInformObser;
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

    public partial class TwistRegularDataLibrary : ITwistRegularDataLibrary
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

    public partial class TwistRegularDataLibrary
    {
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

                    if(!result.status)
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
    }

    public partial class TwistRegularDataLibrary
    {
        public async Task NotifyTwistUpdate()
        {
            await ITwistInformObser.NotifyTwistUpdated(ITwistTableLibrary.ListTwistInQueue);
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

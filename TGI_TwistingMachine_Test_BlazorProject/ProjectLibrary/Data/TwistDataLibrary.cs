using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data
{
    public partial class TwistDataLibrary
    {
        readonly ILogsTableLibrary ILogTableLibrary;

        readonly ITwistTableLibrary ITwistTableLibrary;

        readonly ISystemObserverable ISysObser;
        readonly ITwistInformObserverable ITwistInformObser;
        readonly INLogObservable INLogObser;

        public TwistDataLibrary(ILogsTableLibrary ILogTableLibrary,
                                ITwistTableLibrary ITwistTableLibrary,
                                ISystemObserverable ISysObser,
                                ITwistInformObserverable ITwistInformObser,
                                INLogObservable INLogObser)
        {
            this.ILogTableLibrary = ILogTableLibrary;
            this.ITwistTableLibrary = ITwistTableLibrary;

            this.ISysObser = ISysObser;
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

    public partial class TwistDataLibrary : ITwistDataLibrary
    {
        Dictionary<ETwist, ushort> _dcTwistMissionCmd { get; set; } = new Dictionary<ETwist, ushort>();

        public Dictionary<ETwist, ushort> DcTwistMissionCmd
        {
            get
            {
                return _dcTwistMissionCmd;
            }
            set
            {
                _dcTwistMissionCmd = value;
            }
        }

        Dictionary<ETwist, ushort> _dcTwistAutoCmd { get; set; } = new Dictionary<ETwist, ushort>();

        public Dictionary<ETwist, ushort> DcTwistAutoCmd
        {
            get
            {
                return _dcTwistAutoCmd;
            }
            set
            {
                _dcTwistAutoCmd = value;
            }
        }

        Dictionary<ETwist, TwistStatusDto> _dcTwistStatusDto { get; set; } = new Dictionary<ETwist, TwistStatusDto>();

        public Dictionary<ETwist, TwistStatusDto> DcTwistStatusDto
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

    public partial class TwistDataLibrary
    {
        public async Task<bool> UpsertTwistTable()
        {
            Dictionary<ETwist, TwistingMachineTable> _dcTwistInQueue = ITwistTableLibrary.ListTwistInQueue
                                                                                         .ToDictionary(x => x.Machine);

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
            bool isChange = dto.machineNumber != table.MachineNumber
                            || dto.processState != table.ProcessState
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
                Machine = table.Machine,
                MachineNumber = dto.machineNumber,
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

    public partial class TwistDataLibrary
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

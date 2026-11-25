using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.Scope;

namespace TGI_TwistingMachine_Test_BlazorProject.Service.TwistMissionService
{
    public partial class TwistMissionService
    {
        readonly TgiScope scope;

        public TwistMissionService(TgiScope scope)
        {
            this.scope = scope;

            scope.twistMissionApp.dgTgiLogsUpdated += HandleLogUpdated;
            scope.twistMissionApp.dgTgiDcTwistUpdated += HandleDcTwistUpdated;
        }

        public event dgLogsUpdated dgLogsUpdated;
        public event dgDcTwistUpdated dgDcTwistUpdated;
    }

    public delegate Task dgLogsUpdated(List<LogTable> listLog);
    public delegate Task dgDcTwistUpdated(Dictionary<ETwist, TwistingMachineTable> dcTwist);

    public partial class TwistMissionService : ITwistMissionService
    {
        public TwistingMachineTable GetTwistTableInQueue(ETwist twist)
        {
            return scope.twistMissionApp.GetTwistTableInQueue(twist);
        }

        public List<LogTable> GetListLogTableInQueue(ETwist twist)
        {
            return scope.twistMissionApp.GetListLogTableInQueue(twist);
        }

        public bool SetMissionCompleted(ETwist twist, int completed)
        {
            return scope.twistMissionApp.SetMissionCompleted(twist, completed);
        }

        public bool SetAutoManualMode(ETwist twist, int mode)
        {
            return scope.twistMissionApp.SetAutoManualMode(twist, mode);
        }
    }

    public partial class TwistMissionService
    {
        public async Task HandleLogUpdated(List<LogTable> list)
        {
            dgLogsUpdated?.Invoke(list);
        }

        public async Task HandleDcTwistUpdated(Dictionary<ETwist, TwistingMachineTable> dcTwist)
        {
            dgDcTwistUpdated?.Invoke(dcTwist);
        }
    }
}

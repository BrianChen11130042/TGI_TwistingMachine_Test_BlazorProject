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
        public event dgCmdResult dgCmdResult;
    }

    public delegate Task dgLogsUpdated(List<LogTable> listLog);
    public delegate Task dgDcTwistUpdated(Dictionary<ETwist, TwistingMachineTable> dcTwist);
    public delegate Task dgCmdResult(bool success, string msg);

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

        public void SetMissionCompleted(ETwist twist, int completed)
        {
            if(scope.twistMissionApp.SetMissionCompleted(twist, completed))
            {
                dgCmdResult?.Invoke(true, $"Set {twist} mission {completed} completed successfully");
            }
            else
            {
                dgCmdResult?.Invoke(false, $"Failed to set {twist} mission {completed} completed");
            }
        }

        public async Task SetAutoManualMode(ETwist twist, int mode)
        {
            if(mode < 0 || mode > 1)
            {
                dgCmdResult?.Invoke(false, $"Failed to set {twist} mode {mode}");
                return;
            }

            if (await scope.twistMissionApp.SetAutoManualMode(twist, mode))
            {
                dgCmdResult?.Invoke(true, $"Set {twist} mode {mode} successfully");
            }
            else
            {
                dgCmdResult?.Invoke(false, $"Failed to set {twist} mode {mode}");
            }
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

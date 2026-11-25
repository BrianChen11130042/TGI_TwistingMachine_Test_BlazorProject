using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Service.TwistMissionService
{
    public interface ITwistMissionService
    {
        TwistingMachineTable GetTwistTableInQueue(ETwist twist);

        List<LogTable> GetListLogTableInQueue(ETwist twist);

        bool SetMissionCompleted(ETwist twist, int completed);

        bool SetAutoManualMode(ETwist twist, int mode);

        event dgLogsUpdated dgLogsUpdated;

        event dgDcTwistUpdated dgDcTwistUpdated;
    }
}

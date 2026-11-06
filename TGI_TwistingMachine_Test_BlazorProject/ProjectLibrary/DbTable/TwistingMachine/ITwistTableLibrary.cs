using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine
{
    public interface ITwistTableLibrary
    {
        List<TwistingMachineTable> ListTwistInQueue { get; set; }

        Task<(bool status, string msg)> InitListTwistInQueue();

        Task<(bool status, string msg)> UpsertTwistTable(TwistingMachineTable data);

        void UpsertTwistTableInQueue(TwistingMachineTable data);
    }
}

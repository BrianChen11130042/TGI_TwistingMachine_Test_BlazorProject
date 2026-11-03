using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine
{
    public interface ITwistTableLibrary
    {
        List<TwistingMachineTable> ListTwistInQueue { get; set; }

        Task<(bool status, string msg)> InitTwistInfromInQueue();
    }
}

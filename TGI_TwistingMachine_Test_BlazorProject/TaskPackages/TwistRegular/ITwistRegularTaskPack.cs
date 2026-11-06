namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistRegular
{
    public interface ITwistRegularTaskPack
    {
        Task<bool> GetTwistStatus();

        Task<bool> UpsertTwistTable();

        Task NotifyTwistUpdate();
    }
}

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Initial
{

    public interface IInitialTaskPack
    {
        Task<bool> InitSystem();

        Task NotifyTwistUpdate();

        Task NotifyLogUpdate();

        Task NotifyInitialSuccess();

        Task NotifyInitialFail();
    }
}

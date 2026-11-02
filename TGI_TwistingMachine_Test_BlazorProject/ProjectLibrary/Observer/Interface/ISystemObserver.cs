namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface
{
    public interface ISystemObserverable
    {
        void AddSystemObserver(ISystemObserver o);

        void RemoveSystemObserver(ISystemObserver o);

        Task NotifyInitResult(bool success, string msg);

        Task NotifyDisconnect();
    }

    public interface ISystemObserver
    {
        Task HandleInitResult(bool success, string msg);

        Task HandleDisconnect();
    }
}

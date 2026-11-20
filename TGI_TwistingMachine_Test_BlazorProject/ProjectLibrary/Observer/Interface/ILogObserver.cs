using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface
{
    public interface ILogObservable
    {
        void AddLogObserver(ILogObserver o);

        void RemoveLogObserver(ILogObserver o);

        Task NotifyLogUpdated(List<LogTable> list);
    }

    public interface ILogObserver
    {
        Task HandleLogUpdated(List<LogTable> list);
    }
}

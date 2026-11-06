using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface
{
    public interface ITwistInformObserverable
    {
        void AddTwistInformObserver(ITwistInformObserver o);

        void RemoveTwistInformObserver(ITwistInformObserver o);

        Task NotifyTwistUpdated(List<TwistingMachineTable> list);
    }

    public interface ITwistInformObserver
    {
        Task HandleTwistUpdated(List<TwistingMachineTable> list);
    }
}

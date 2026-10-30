using Microsoft.Extensions.DependencyInjection;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public ObserverLibrary observerLibrary { get; set; }

        void _createObserverLibrary()
        {
            observerLibrary = provider.GetRequiredService<ObserverLibrary>();
        }

        void _initObserverLibrary()
        {
            observerLibrary.AddNLogObserver(logger);
        }
    }
}

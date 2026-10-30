using CommonRazorLibraryB.Tools.NLogWritter;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer
{
    public partial class ObserverLibrary : INLogObservable
    {
        List<INLogObserver> os { get; set; }

        public void AddNLogObserver(INLogObserver o)
        {
            if (os is null)
                os = new List<INLogObserver>();

            if(!os.Contains(o))
            {
                os.Add(o);
            }
        }

        public void RemoveNLogObserver(INLogObserver o)
        {
            if(os is not null && os.Contains(o))
            {
                os.Remove(o);
            }
        }

        public async Task NotifyNLog(ENLogLevel level, string msg)
        {
            if(os is not null)
            {
                foreach(var o in os)
                {
                    await o.HandleNLog(level, msg);
                }
            }
        }
    }
}

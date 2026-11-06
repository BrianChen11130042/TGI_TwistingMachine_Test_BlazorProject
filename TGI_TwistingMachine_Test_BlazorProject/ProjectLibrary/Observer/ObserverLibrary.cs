using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer
{
    public partial class ObserverLibrary : INLogObservable
    {
        List<INLogObserver> osNLog { get; set; }

        public void AddNLogObserver(INLogObserver o)
        {
            if (osNLog is null)
                osNLog = new List<INLogObserver>();

            if(!osNLog.Contains(o))
            {
                osNLog.Add(o);
            }
        }

        public void RemoveNLogObserver(INLogObserver o)
        {
            if(osNLog is not null && osNLog.Contains(o))
            {
                osNLog.Remove(o);
            }
        }

        public async Task NotifyNLog(ENLogLevel level, string msg)
        {
            if(osNLog is not null)
            {
                foreach(var o in osNLog)
                {
                    await o.HandleNLog(level, msg);
                }
            }
        }
    }

    public partial class ObserverLibrary : ISystemObserverable
    {
        List<ISystemObserver> osSystem { get; set; }

        public void AddSystemObserver(ISystemObserver o)
        {
            if (osSystem == null)
                osSystem = new List<ISystemObserver>();

            if(!osSystem.Contains(o))
            {
                osSystem.Add(o);
            }
        }

        public void RemoveSystemObserver(ISystemObserver o)
        {
            if(osSystem != null && osSystem.Contains(o))
            {
                osSystem.Remove(o);
            }
        }

        public async Task NotifyInitResult(bool success, string msg)
        {
            if(osSystem != null)
            {
                foreach(var o in osSystem)
                {
                    await o.HandleInitResult(success, msg);
                }
            }
        }

        public async Task NotifyDisconnect()
        {
            if(osSystem != null)
            {
                foreach(var o in osSystem)
                {
                    await o.HandleDisconnect();
                }
            }
        }
    }

    public partial class ObserverLibrary : ITwistInformObserverable
    {
        List<ITwistInformObserver> osTwist { get; set; }

        public void AddTwistInformObserver(ITwistInformObserver o)
        {
            if (osTwist == null)
                osTwist = new List<ITwistInformObserver>();

            if(!osTwist.Contains(o))
            {
                osTwist.Add(o);
            }
        }

        public void RemoveTwistInformObserver(ITwistInformObserver o)
        {
            if(osTwist != null && osTwist.Contains(o))
            {
                osTwist.Remove(o);
            }
        }

        public async Task NotifyTwistUpdated(List<TwistingMachineTable> list)
        {
            if(osTwist != null)
            {
                foreach(var o in osTwist)
                {
                    await o.HandleTwistUpdated(list);
                }
            }
        }
    }
}

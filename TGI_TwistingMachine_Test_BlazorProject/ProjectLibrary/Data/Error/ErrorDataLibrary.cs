using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Observer.Interface;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Error
{
    public partial class ErrorDataLibrary
    {
        readonly ILogsTableLibrary ILogsTableLibrary;

        readonly ISystemObservable ISysObser;
        readonly ILogObservable ILogObser;
        readonly INLogObservable INLogObser;

        public ErrorDataLibrary(ILogsTableLibrary ILogsTableLibrary,
                                ISystemObservable ISysObser,
                                ILogObservable ILogObser,
                                INLogObservable INLogObser)
        {
            this.ILogsTableLibrary = ILogsTableLibrary;
            this.ISysObser = ISysObser;
            this.ILogObser = ILogObser;
            this.INLogObser = INLogObser;
        }

        async Task _writeNLogError(string log)
        {
            await INLogObser.NotifyNLog(ENLogLevel.Error, log);
        }

        async Task _writeNLogInform(string log)
        {
            await INLogObser.NotifyNLog(ENLogLevel.Info, log);
        }
    }

    public partial class ErrorDataLibrary : IErrorDataLibrary
    {
        public async Task NotifySysDisconnect()
        {
            await ISysObser.NotifyDisconnect();
        }

        public async Task WriteNLogError(string log)
        {
            await _writeNLogError(log);
        }

        public async Task WriteNLogInform(string log)
        {
            await _writeNLogInform(log);
        }
    }
}

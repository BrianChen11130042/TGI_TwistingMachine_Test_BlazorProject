using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Error;

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.SystemControl.Error
{
    public partial class ErrorTaskPack
    {
        readonly IErrorDataLibrary IDataLib;

        public ErrorTaskPack(IErrorDataLibrary IDataLib)
        {
            this.IDataLib = IDataLib;
        }

        const string info = "Inform";

        const string err = "Error";
    }

    public partial class ErrorTaskPack : IErrorTaskPack
    {
        public async Task NotifyDisconnect()
        {
            await IDataLib.NotifySysDisconnect();
        }
    }
}

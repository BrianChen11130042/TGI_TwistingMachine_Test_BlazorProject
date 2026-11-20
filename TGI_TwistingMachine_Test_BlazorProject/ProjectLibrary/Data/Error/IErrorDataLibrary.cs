namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Error
{
    public interface IErrorDataLibrary
    {
        Task NotifySysDisconnect();

        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

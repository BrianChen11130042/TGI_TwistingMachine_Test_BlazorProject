using TGI_TwistingMachine_Test_BlazorProject.DtoModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistRegular
{
    public interface ITwistRegularDataLibrary
    {
        Dictionary<int, TwistStatusDto> DcTwistStatusDto { get; set; }

        Task<bool> UpsertTwistTable();

        Task NotifyTwistUpdate();

        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

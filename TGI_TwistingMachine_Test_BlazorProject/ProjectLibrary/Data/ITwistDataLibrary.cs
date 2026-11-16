using TGI_TwistingMachine_Test_BlazorProject.DtoModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data
{
    public interface ITwistDataLibrary
    {
        Dictionary<int, ushort> DcTwistMissionCmd { get; set; }

        Dictionary<int, ushort> DcTwistAutoCmd { get; set; }

        Dictionary<int, TwistStatusDto> DcTwistStatusDto { get; set; }

        Task<bool> UpsertTwistTable();

        Task NotifyTwistUpdate();

        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

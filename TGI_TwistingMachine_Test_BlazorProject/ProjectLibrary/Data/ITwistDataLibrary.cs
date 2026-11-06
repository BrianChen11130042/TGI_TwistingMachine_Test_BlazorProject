using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data
{
    public interface ITwistDataLibrary
    {
        Dictionary<ETwist, ushort> DcTwistMissionCmd { get; set; }

        Dictionary<ETwist, ushort> DcTwistAutoCmd { get; set; }

        Dictionary<ETwist, TwistStatusDto> DcTwistStatusDto { get; set; }

        Task<bool> UpsertTwistTable();

        Task NotifyTwistUpdate();

        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

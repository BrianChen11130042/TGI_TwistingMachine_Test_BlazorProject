using TGI_TwistingMachine_Test_BlazorProject.DtoModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.Initial
{
    public interface IInitialDataLibrary
    {
        Dictionary<int, TwistStatusDto> DcTwistStatusDto { get; set; }

        Task<bool> InitTwistTableInQueue();

        Task<bool> CheckTwistTableData();

        Task<bool> UpsertTwistTable();

        Task<bool> InitLogTableInQueue();

        Task NotifyInitialResult(bool success, string msg);

        Task NotifyTwistUpdate();

        Task NotifyLogUpdate();

        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

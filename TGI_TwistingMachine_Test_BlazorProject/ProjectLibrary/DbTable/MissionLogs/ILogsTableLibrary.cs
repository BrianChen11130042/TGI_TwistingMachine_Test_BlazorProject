using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs
{
    public interface ILogsTableLibrary
    {
        List<LogTable> listLogDataInQueue { get; set; }

        Task<(bool status, string msg)> InitLogDataInQueue();

        Task<(bool status, string msg)> AddLogData(LogTable data);
    }
}

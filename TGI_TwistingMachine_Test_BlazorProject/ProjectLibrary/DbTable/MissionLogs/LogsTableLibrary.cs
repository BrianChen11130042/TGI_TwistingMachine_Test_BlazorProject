using Microsoft.EntityFrameworkCore;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.MissionLogs
{
    public partial class LogsTableLibrary
    {
        readonly IServiceProvider serviceProvider;

        public LogsTableLibrary(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        List<LogTable> _listLogDataInQueue { get; set; } = new List<LogTable>();

        public List<LogTable> listLogDataInQueue
        {
            get
            {
                return _listLogDataInQueue;
            }
            set
            {
                _listLogDataInQueue = value;
            }
        }
    }

    public partial class LogsTableLibrary : ILogsTableLibrary
    {
        public async Task<(bool status, string msg)> InitLogDataInQueue()
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    DateTime timePoint = DateTime.Now.AddDays(-5);

                    List<LogTable> list = await context.LogTables.AsNoTracking()
                                                                 .Where(x => x.RecordTime >= timePoint)
                                                                 .OrderByDescending(x => x.RecordTime)
                                                                 .Take(100)
                                                                 .ToListAsync();

                    listLogDataInQueue.Clear();
                    listLogDataInQueue = list.ToList();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string msg)> AddLogData(LogTable data)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    if (data.RecordTime == default)
                        data.RecordTime = DateTime.Now;

                    context.LogTables.Add(data);

                    await context.SaveChangesAsync();

                    _AddLogDataInQueue(data);

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        void _AddLogDataInQueue(LogTable data)
        {

            listLogDataInQueue.Add(data);

            DateTime timePoint = DateTime.Now.AddDays(-5);

            listLogDataInQueue = listLogDataInQueue.Where(x => x.RecordTime >= timePoint)
                                     .OrderByDescending(x => x.RecordTime)
                                     .Take(100)
                                     .ToList();
        }
    }
}

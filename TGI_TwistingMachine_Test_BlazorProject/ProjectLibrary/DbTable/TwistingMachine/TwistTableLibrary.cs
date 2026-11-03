using Microsoft.EntityFrameworkCore;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine
{
    public partial class TwistTableLibrary
    {
        readonly IServiceProvider serviceProvider;

        public TwistTableLibrary(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        List<TwistingMachineTable> _listTwistInQueue { get; set; } = new List<TwistingMachineTable>();

        public List<TwistingMachineTable> ListTwistInQueue
        {
            get
            {
                return _listTwistInQueue;
            }
            set
            {
                _listTwistInQueue = value;
            }
        }
    }

    public partial class TwistTableLibrary : ITwistTableLibrary
    {
        public async Task<(bool status, string msg)> InitTwistInfromInQueue()
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    List<TwistingMachineTable> list = await context.TwistingMachineTables.Include(x => x.CakeTables)
                                                                                         .Include(x => x.BobbinTables)
                                                                                         .AsNoTracking()
                                                                                         .OrderBy(x => x.Machine)
                                                                                         .ToListAsync();

                    ListTwistInQueue.Clear();
                    ListTwistInQueue = list.ToList();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

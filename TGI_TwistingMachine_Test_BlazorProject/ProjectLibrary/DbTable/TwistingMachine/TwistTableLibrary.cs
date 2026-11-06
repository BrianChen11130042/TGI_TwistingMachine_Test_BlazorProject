using Microsoft.EntityFrameworkCore;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

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
        public async Task<(bool status, string msg)> InitListTwistInQueue()
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

        public async Task<(bool status, string msg)> UpsertTwistTable(TwistingMachineTable data)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    TwistingMachineTable target = await context.TwistingMachineTables.FirstOrDefaultAsync(x => x.Machine == data.Machine);

                    if(target != null)
                    {
                        context.Entry(target).CurrentValues.SetValues(data);
                    }
                    else
                    {
                        context.TwistingMachineTables.Add(data);
                    }

                    await context.SaveChangesAsync();

                    UpsertTwistTableInQueue(data);

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public void UpsertTwistTableInQueue(TwistingMachineTable data)
        {
            var target = ListTwistInQueue.FirstOrDefault(x => x.Machine == data.Machine);

            if(target != null)
            {
                target.MachineNumber = data.MachineNumber;
                target.ProcessState = data.ProcessState;
                target.MachineStatus = data.MachineStatus;
                target.MissionStartedInform = data.MissionStartedInform;
                target.MissionCompletedInform = data.MissionCompletedInform;
                target.AutoMode = data.AutoMode;
            }
            else
            {
                ListTwistInQueue.Add(data);
            }
        }
    }
}

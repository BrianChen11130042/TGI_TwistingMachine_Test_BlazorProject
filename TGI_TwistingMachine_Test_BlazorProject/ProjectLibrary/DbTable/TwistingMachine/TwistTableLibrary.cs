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
        public async Task<(bool status, string msg)> InitListTwistInQueue()
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    List<TwistingMachineTable> list = await context.TwistingMachineTables.Include(x => x.CakeTables)
                                                                                         .Include(x => x.BobbinTables)
                                                                                         .AsSplitQuery()
                                                                                         .AsNoTracking()
                                                                                         .OrderBy(x => x.MachineNumber)
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

                    TwistingMachineTable? target = await context.TwistingMachineTables
                                                                .FirstOrDefaultAsync(x => x.Id == data.Id);

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
            var target = ListTwistInQueue.FirstOrDefault(x => x.Id == data.Id);

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

    #region 設定UI用

    public partial class TwistTableLibrary
    {
        public async Task<(bool status, string msg, List<TwistingMachineTable> list)> GetListTwistTableFromDb()
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    List<TwistingMachineTable> list = await context.TwistingMachineTables.Include(x => x.CakeTables)
                                                                                         .Include(x => x.BobbinTables)
                                                                                         .AsSplitQuery()
                                                                                         .AsNoTracking()
                                                                                         .OrderBy(x => x.MachineNumber)
                                                                                         .ToListAsync();

                    return (true, string.Empty, list.ToList());
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message, new List<TwistingMachineTable>());
            }
        }

        public async Task<(bool status, string msg)> UpsertListTwistTableToDb(List<TwistingMachineTable> listData)
        {
            try
            {
                if (listData is null || listData.Count == 0)
                    return (true, string.Empty);

                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    foreach(TwistingMachineTable item in listData)
                    {
                        TwistingMachineTable? target = await context.TwistingMachineTables
                                                                    .FirstOrDefaultAsync(x => x.Id == item.Id);

                        if(target is not null)
                        {
                            context.Entry(target).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            context.TwistingMachineTables.Add(item);
                        }
                    }

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string msg)> DeleteListTwistTableToDb(List<Guid> listId)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    foreach(Guid id in listId)
                    {
                        List<TwistingMachineTable> listTarget = await context.TwistingMachineTables
                                                                         .Include(x => x.CakeTables)
                                                                         .Include(x => x.BobbinTables)
                                                                         .AsSplitQuery()
                                                                         .Where(x => x.Id == id)
                                                                         .ToListAsync();

                        context.TwistingMachineTables.RemoveRange(listTarget);
                    }

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }

    public partial class TwistTableLibrary
    {
        public async Task<(bool status, string msg)> DeleteListCakeTableToDb(List<Guid> listId)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    List<CakeTable> listTarget = await context.CakeTables.Where(x => listId.Contains(x.Id))
                                                                         .ToListAsync();

                    if(listTarget.Count > 0)
                        context.CakeTables.RemoveRange(listTarget);

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string msg)> UpsertListCakeTableToDb(List<CakeTable> listData)
        {
            try
            {
                if (listData is null || listData.Count == 0)
                    return (false, "No cake data to upsert");

                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    foreach(CakeTable item in listData)
                    {
                        CakeTable? target = await context.CakeTables
                                                         .FirstOrDefaultAsync(x => x.Id == item.Id);

                        if(target is not null)
                        {
                            context.Entry(target).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            context.CakeTables.Add(item);
                        }
                    }

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch(Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }

    public partial class TwistTableLibrary
    {
        public async Task<(bool status, string msg)> DeleteListBobbinTableToDb(List<Guid> listId)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    List<BobbinTable> listTarget = await context.BobbinTables.Where(x => listId.Contains(x.Id))
                                                                             .ToListAsync();

                    if (listTarget.Count > 0)
                        context.BobbinTables.RemoveRange(listTarget);

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool status, string msg)> UpsertListBobbinTableToDb(List<BobbinTable> listData)
        {
            try
            {
                if (listData is null || listData.Count == 0)
                    return (false, "No bobbin data to upsert");

                using (var scope = serviceProvider.CreateScope())
                {
                    TwistDbContext context = scope.ServiceProvider.GetRequiredService<TwistDbContext>();

                    foreach (BobbinTable item in listData)
                    {
                        BobbinTable? target = await context.BobbinTables
                                                           .FirstOrDefaultAsync(x => x.Id == item.Id);

                        if (target is not null)
                        {
                            context.Entry(target).CurrentValues.SetValues(item);
                        }
                        else
                        {
                            context.BobbinTables.Add(item);
                        }
                    }

                    await context.SaveChangesAsync();

                    return (true, string.Empty);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }

    #endregion
}

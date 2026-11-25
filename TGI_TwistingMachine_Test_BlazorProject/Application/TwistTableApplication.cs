using CommonRazorLibraryB.Tools.NLogWritter;
using TGI_TwistingMachine_Test_BlazorProject.EFModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.DbTable.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Application
{
    public partial class TwistTableApplication
    {
        readonly TwistTableLibrary twistTableLibrary;

        readonly INLogObservable INLogObser;

        public TwistTableApplication(TwistTableLibrary twistTableLibrary,
                                     INLogObservable INLogObser)
        {
            this.twistTableLibrary = twistTableLibrary;
            this.INLogObser = INLogObser;
        }
    }

    public partial class TwistTableApplication
    {
        public async Task<(bool status, List<TwistingMachineTable> list)> GetTwistTableConfig()
        {
            var result = await twistTableLibrary.GetListTwistTableFromDb();

            if(result.status)
            {
                return (result.status, result.list);
            }
            else
            {
                await INLogObser.NotifyNLog(ENLogLevel.Error, result.msg);
                return (result.status, result.list);
            }
        }

        public async Task<bool> SetTwistTableConfig(List<TwistingMachineTable> listData)
        {
            if (!await _checkListTwistAndPortsData(listData))
                return false;

            var dbResult = await twistTableLibrary.GetListTwistTableFromDb();

            if (!dbResult.status)
            {
                await INLogObser.NotifyNLog(ENLogLevel.Error, dbResult.msg);
                return dbResult.status;
            }

            List<TwistingMachineTable> listDb = dbResult.list.ToList();
            List<Guid> listDbId = listDb.Select(x => x.Id).Distinct().ToList();
            List<Guid> listDataId = listData.Select(x => x.Id).Distinct().ToList();

            if (!await _deleteTwistTable(listDbId, listDataId))
                return false;

            if (!await _upsertTwistTable(listData, listDb))
                return false;

            return true;
        } 
    }

    public partial class TwistTableApplication
    {
        async Task<bool> _checkListTwistAndPortsData(List<TwistingMachineTable> listData)
        {
            bool hasDuplicateMachineNumber = listData.GroupBy(x => x.MachineNumber)
                                                     .Any(g => g.Count() > 1);
            if (hasDuplicateMachineNumber)
            {
                await INLogObser.NotifyNLog(ENLogLevel.Error, "Duplicate MachineNumber");
                return false;
            }

            foreach (TwistingMachineTable twist in listData)
            {
                bool hasDuplicateCakePort = twist.CakeTables.GroupBy(x => x.PortNumber)
                                                            .Any(g => g.Count() > 1);
                if (hasDuplicateCakePort)
                {
                    await INLogObser.NotifyNLog(ENLogLevel.Error,
                                                  $"Duplicate Cake PortNumber on Twist {twist.MachineNumber}");
                    return false;
                }


                bool hasDuplicateBobbinPort = twist.BobbinTables.GroupBy(x => x.PortNumber)
                                                                .Any(g => g.Count() > 1);
                if (hasDuplicateBobbinPort)
                {
                    await INLogObser.NotifyNLog(ENLogLevel.Error,
                                                  $"Duplicate Bobbin PortNumber on Twist {twist.MachineNumber}");
                    return false;
                }
            }

            return true;
        }


        async Task<bool> _deleteTwistTable(List<Guid> listDbId, List<Guid> listDataId)
        {
            List<Guid> listDelete = listDbId.Where(x => !listDataId.Contains(x)).ToList();

            var result = await twistTableLibrary.DeleteListTwistTableToDb(listDelete);

            if(result.status)
            {
                return result.status;
            }
            else
            {
                await INLogObser.NotifyNLog(ENLogLevel.Error, result.msg);

                return result.status;
            }
        }

        async Task<bool> _upsertTwistTable(List<TwistingMachineTable> listData, List<TwistingMachineTable> listDb)
        {
            var upsertTwistResult = await twistTableLibrary.UpsertListTwistTableToDb(listData);

            if(!upsertTwistResult.status)
            {
                await INLogObser.NotifyNLog(ENLogLevel.Error, upsertTwistResult.msg);
                return upsertTwistResult.status;
            }

            foreach(TwistingMachineTable dataTwist in listData)
            {
                TwistingMachineTable? dbTwist = listDb.FirstOrDefault(x => x.Id == dataTwist.Id);

                if (!await _syncCakeTable(dataTwist, dbTwist))
                    return false;

                if (!await _syncBobbinTable(dataTwist, dbTwist))
                    return false;
            }

            return true;
        }

        async Task<bool> _syncCakeTable(TwistingMachineTable dataTwist, TwistingMachineTable? dbTwist)
        {
            if(dbTwist is not null)
            {
                List<Guid> listDataCakeId = dataTwist.CakeTables.Select(x => x.Id).Distinct().ToList();

                List<Guid> listDeleteCakeId = dbTwist.CakeTables.Where(x => !listDataCakeId.Contains(x.Id))
                                                                 .Select(x => x.Id)
                                                                 .Distinct()
                                                                 .ToList();

                if(listDeleteCakeId.Count > 0)
                {
                    var deleteResult = await twistTableLibrary.DeleteListCakeTableToDb(listDeleteCakeId);

                    if(!deleteResult.status)
                    {
                        await INLogObser.NotifyNLog(ENLogLevel.Error, deleteResult.msg);
                        return deleteResult.status;
                    }
                }
            }

            foreach (CakeTable cake in dataTwist.CakeTables)
            {
                cake.MachineId = dataTwist.Id;
            }

            if (dataTwist.CakeTables.Count > 0)
            {
                var upsertRsult = await twistTableLibrary.UpsertListCakeTableToDb(dataTwist.CakeTables.ToList());

                if(!upsertRsult.status)
                {
                    await INLogObser.NotifyNLog(ENLogLevel.Error, upsertRsult.msg);
                    return upsertRsult.status;
                }
            }

            return true;
        }

        async Task<bool> _syncBobbinTable(TwistingMachineTable dataTwist, TwistingMachineTable? dbTwist)
        {
            if (dbTwist is not null)
            {
                List<Guid> listDataBobbinId = dataTwist.BobbinTables.Select(x => x.Id).Distinct().ToList();

                List<Guid> listDeleteBobbinId = dbTwist.BobbinTables.Where(x => !listDataBobbinId.Contains(x.Id))
                                                                    .Select(x => x.Id)
                                                                    .Distinct()
                                                                    .ToList();

                if (listDeleteBobbinId.Count > 0)
                {
                    var deleteResult = await twistTableLibrary.DeleteListBobbinTableToDb(listDeleteBobbinId);

                    if (!deleteResult.status)
                    {
                        await INLogObser.NotifyNLog(ENLogLevel.Error, deleteResult.msg);
                        return deleteResult.status;
                    }
                }
            }

            foreach (BobbinTable bobbin in dataTwist.BobbinTables)
            {
                bobbin.MachineId = dataTwist.Id;
            }

            if (dataTwist.BobbinTables.Count > 0)
            {
                var upsertRsult = await twistTableLibrary.UpsertListBobbinTableToDb(dataTwist.BobbinTables.ToList());

                if (!upsertRsult.status)
                {
                    await INLogObser.NotifyNLog(ENLogLevel.Error, upsertRsult.msg);
                    return upsertRsult.status;
                }
            }

            return true;
        }

    }
}

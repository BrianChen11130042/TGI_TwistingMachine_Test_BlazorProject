using CommonRazorLibraryB.Library.TGITwistingMachine.Adapter;
using CommonRazorLibraryB.Library.TGITwistingMachine.Package;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistRegular;

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistRegular
{
    public partial class TwistRegularTaskPack<ETwist>
    {
        readonly List<ETwist> listTwist;

        readonly ITgiTwistPackage<ETwist> ITwistPack;
        readonly ITgiTwistAdapter<ETwist> ITwistOp;

        readonly ITwistRegularDataLibrary IDataLib;

        public TwistRegularTaskPack(List<ETwist> listTwist,
                                    ITgiTwistPackage<ETwist> ITwistPack,
                                    ITgiTwistAdapter<ETwist> ITwistOp,
                                    ITwistRegularDataLibrary IDataLib)
        {
            this.listTwist = listTwist;

            this.ITwistPack = ITwistPack;
            this.ITwistOp = ITwistOp;

            this.IDataLib = IDataLib;
        }
    }

    public partial class TwistRegularTaskPack<ETwist> : ITwistRegularTaskPack
    {
        public async Task<bool> GetTwistStatus()
        {
            IDataLib.DcTwistStatusDto.Clear();

            foreach (ETwist twist in listTwist)
            {
                await ITwistPack.Packages[twist].gate.WaitAsync();

                try
                {
                    if(await ITwistOp.GetTwistStatus(twist))
                    {
                        int _key = ITwistPack.Packages[twist].property.get.machineNumber;

                        IDataLib.DcTwistStatusDto.Add(_key, new TwistStatusDto()
                        {
                            machineNumber = ITwistPack.Packages[twist].property.get.machineNumber,
                            processState = ITwistPack.Packages[twist].property.get.processState,
                            machineStatus = ITwistPack.Packages[twist].property.get.machineStatus,
                            missionStartedInform = ITwistPack.Packages[twist].property.get.missionStartedInform,
                            missionCompletedInform = ITwistPack.Packages[twist].property.get.missionCompletedInform,
                            autoMode = ITwistPack.Packages[twist].property.get.isAutoMode
                        });
                    }
                    else
                    {
                        string nlog = ITwistPack.Packages[twist].errorLog;
                        await IDataLib.WriteNLogError(nlog);
                        return false;
                    }
                }
                finally
                {
                    ITwistPack.Packages[twist].gate.Release();
                }
            }

            return true;
        }

        public async Task<bool> UpsertTwistTable()
        {
            return await IDataLib.UpsertTwistTable();
        }

        public async Task NotifyTwistUpdate()
        {
            await IDataLib.NotifyTwistUpdate();
        }

        public async Task NotifyLogUpdate()
        {
            await IDataLib.NotifyLogUpdate();
        }
    }
}

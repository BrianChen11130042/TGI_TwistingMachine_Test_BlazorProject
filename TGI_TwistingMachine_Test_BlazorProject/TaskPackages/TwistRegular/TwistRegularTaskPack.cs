using CommonRazorLibraryB.Library.TGITwistingMachine.Adapter;
using CommonRazorLibraryB.Library.TGITwistingMachine.Package;
using TGI_TwistingMachine_Test_BlazorProject.DtoModel;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;
using TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data;

namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistRegular
{
    public partial class TwistRegularTaskPack
    {
        readonly List<ETwist> listTwist;

        readonly ITgiTwistPackage<ETwist> ITwistPack;
        readonly ITgiTwistAdapter<ETwist> ITwistOp;

        readonly ITwistDataLibrary IDataLib;

        public TwistRegularTaskPack(List<ETwist> listTwist,
                                    ITgiTwistPackage<ETwist> ITwistPack,
                                    ITgiTwistAdapter<ETwist> ITwistOp,
                                    ITwistDataLibrary IDataLib)
        {
            this.listTwist = listTwist;

            this.ITwistPack = ITwistPack;
            this.ITwistOp = ITwistOp;

            this.IDataLib = IDataLib;
        }
    }

    public partial class TwistRegularTaskPack : ITwistRegularTaskPack
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
                        IDataLib.DcTwistStatusDto.Add(twist, new TwistStatusDto()
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
            if(await IDataLib.UpsertTwistTable())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task NotifyTwistUpdate()
        {
            await IDataLib.NotifyTwistUpdate();
        }
    }
}

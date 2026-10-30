using CommonRazorLibraryB.Library.TGITwistingMachine;
using CommonRazorLibraryB.Library.TGITwistingMachine.Config;
using CommonRazorLibraryB.Library.TGITwistingMachine.Property;
using Microsoft.Extensions.DependencyInjection;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public TgiTwistConfigManager<ETwist> twistConfig { get; set; }
        public TgiTwistPropertyManager<ETwist> twistProperty { get; set; }
        public TgiTwistLibrary<ETwist> twistLibrary { get; set; }

        void _createTwist()
        {
            twistConfig = provider.GetRequiredService<TgiTwistConfigManager<ETwist>>();
            twistProperty = provider.GetRequiredService<TgiTwistPropertyManager<ETwist>>();
            twistLibrary = provider.GetRequiredService<TgiTwistLibrary<ETwist>>();
        }

        void _initTwist()
        {
            twistLibrary.InitPackage();
            twistLibrary.InitAdapter();
        }
    }
}

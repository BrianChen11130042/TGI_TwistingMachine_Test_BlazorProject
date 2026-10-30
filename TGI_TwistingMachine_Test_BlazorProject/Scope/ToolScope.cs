using CommonRazorLibraryB.Tools.NLogWritter;
using Microsoft.Extensions.DependencyInjection;

namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        public NLogWritter logger { get; set; }

        void _createTool()
        {
            logger = provider.GetRequiredService<NLogWritter>();
        }
    }
}

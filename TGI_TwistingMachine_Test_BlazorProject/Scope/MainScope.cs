namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        IServiceProvider provider;

        public TgiScope(IServiceProvider provider)
        {
            this.provider = provider;
        }
    }
}

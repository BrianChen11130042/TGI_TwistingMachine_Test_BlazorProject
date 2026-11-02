namespace TGI_TwistingMachine_Test_BlazorProject.Scope
{
    public partial class TgiScope
    {
        IServiceProvider provider;

        public TgiScope(IServiceProvider provider)
        {
            this.provider = provider;
            _createAll();
        }

        void _createAll()
        {
            _createTool();
            _createObserverLibrary();
            _createDataLibrary();
            _createManager();
            _createTwist();
            _createApplication();
        }
    }
}

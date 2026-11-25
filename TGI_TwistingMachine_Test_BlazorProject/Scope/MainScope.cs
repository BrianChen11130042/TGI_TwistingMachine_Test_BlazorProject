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

        public void InitAll()
        {
            _stopThread();

            _initObserverLibrary();
            _initManager();
            _initTwist();

            _initApplication();

            _initSysControlTask();
            _initMissionTask();
            _initRegularTask();

            _initThread();
            _startThread();
        }
    }
}

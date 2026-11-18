namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistMission
{
    public interface ITwistMissionTaskPack
    {
        Task GetMachineNumber();

        bool IsGetTwistTableInQueue();

        bool IsAutoModeSwitch();

        bool IsAmrLoadSilkCake();

        bool IsPersonnelLoadEmptyBobbin();

        bool IsPersonnelCutAndCollect();

        bool IsAmrUnloadSilkBobbin();

        bool IsAmrUnloadEmptyCake();

        bool IsPersonnelClean();

        //Auto Mode Switch
        Task<bool> SetAutoModeSwitch();

        Task<bool> AddLogAutoModeSwitch();

        //AmrLoadSilkCake
        Task<bool> AddAmrLoadSilkCakeAssignTable();

        Task<bool> AddLogAmrLoadSilkCakeStart();

        Task<bool> MonitorAmrLoadSilkCakeAssignTable();

        bool IsAmrLoadSilkCakeAssignTableFinish();

        Task<bool> SetAmrLoadSilkCakeComplete();

        Task<bool> AddLogAmrLoadSilkCakeFinish();


        //Common
        void ResetCmdAutoMode();

        void ResetCmdMissionCompleted();
    }
}

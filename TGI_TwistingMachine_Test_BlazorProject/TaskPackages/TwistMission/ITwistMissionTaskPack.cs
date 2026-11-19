namespace TGI_TwistingMachine_Test_BlazorProject.TaskPackages.TwistMission
{
    public interface ITwistMissionTaskPack
    {
        Task GetMachineNumber();

        bool IsGetTwistTableInQueue();

        bool IsAutoModeSwitch();

        bool IsAmrLoadSilkCake();

        bool IsPersonnelLoadNoSilkBobbin();

        bool IsPersonnelCutAndCollect();

        bool IsAmrUnloadSilkBobbin();

        bool IsAmrUnloadNoSilkCake();

        bool IsPersonnelCleanMachine();

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

        //PersonnelLoadNoSilkBobbin
        Task<bool> AddLogPersonnelLoadNoSilkBobbinStart();

        bool IsPersonnelLoadNoSilkBobbinFinish();

        Task<bool> SetBobbinTableNoSilkBobbin();

        Task<bool> SetPersonnelLoadNoSilkBobbinComplete();

        Task<bool> AddLogPersonnelLoadNoSilkBobbinFinish();

        //PersonnelCutAndCollect
        Task<bool> AddLogPersonnelCutAndCollectStart();

        Task<bool> TransferSilkFromCakeToBobbinTable();

        bool IsPersonnelCutAndCollectFinish();

        Task<bool> SetPersonnelCutAndCollectComplete();

        Task<bool> AddLogPersonnelCutAndCollectFinish();

        //AmrUnloadSilkBobbin
        Task<bool> AddAmrUnloadSilkBobbinAssignTable();

        Task<bool> AddLogAmrUnloadSilkBobbinStart();

        Task<bool> MonitorAmrUnloadSilkBobbinAssignTable();

        bool IsAmrUnloadSilkBobbinAssignTableFinish();

        Task<bool> SetAmrUnloadSilkBobbinComplete();

        Task<bool> AddLogAmrUnloadSilkBobbinFinish();

        //AmrUnloadNoSilkCake
        Task<bool> AddAmrUnloadNoSilkCakeAssignTable();

        Task<bool> AddLogAmrUnloadNoSilkCakeStart();

        Task<bool> MonitorAmrUnloadNoSilkCakeAssignTable();

        bool IsAmrUnloadNoSilkCakeAssignTableFinish();

        Task<bool> SetAmrUnloadNoSilkCakeComplete();

        Task<bool> AddLogAmrUnloadNoSilkCakeFinish();

        //PersonnelCleanMachine
        Task<bool> AddLogPersonnelCleanMachineStart();

        bool IsPersonnelCleanMachineFinish();

        Task<bool> SetPersonnelCleanMachineComplete();

        Task<bool> AddLogPersonnelCleanMachineFinish();

        //Common
        void ResetCmdAutoMode();

        void ResetCmdMissionCompleted();
    }
}

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

        Task<bool> SetLogAutoModeSwitch();

        void ResetAutoModeSwitch();
    }
}

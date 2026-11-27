using TGI_TwistingMachine_Test_BlazorProject.EFModel;

namespace TGI_TwistingMachine_Test_BlazorProject.ProjectLibrary.Data.TwistMission
{
    
    public interface ITwistMissionDataLibrary
    {
        int MachineNumber { get; set; }

        // 0 : 復歸 
        // 1 : AMR完成上Silk Cake
        // 2 : 人工完成上NoSilk Bobbin 
        // 4 : 撚紗完成 
        // 8 : 人工完成剪線收線 
        // 16 : Amr完成下Silk Bobbin
        // 32 : Amr完成下NoSilk Cake 
        // 64 : 人工完成清設備
        int CmdMissionCompleted { get; set; } 

        //Twist Table
        TwistingMachineTable TwistTable { get; set; }

        bool GetTwistTableInQueue();

        //Log Table
        Task<bool> AddLogTable(string equip, string type, string log);

        //NLog
        Task WriteNLogError(string log);

        Task WriteNLogInform(string log);
    }
}

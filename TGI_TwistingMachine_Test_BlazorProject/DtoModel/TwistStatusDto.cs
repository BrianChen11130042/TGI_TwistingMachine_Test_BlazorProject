namespace TGI_TwistingMachine_Test_BlazorProject.DtoModel
{
    public class TwistStatusDto
    {
        //撚紗機編號
        public int machineNumber { get; set; }

        //狀態列表一
        public int processState { get; set; }

        //狀態列表二
        public int machineStatus { get; set; }

        //通知作業列表
        public int missionStartedInform { get; set; }

        //完成作業列表
        public int missionCompletedInform { get; set; }

        //自動模式
        public int autoMode { get; set; }
    }
}

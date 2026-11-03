using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public class TwistingMachineTable
    {
        [Key]
        public Guid Id { get; set; }

        //自訂義機台名稱
        public ETwist Machine { get; set; }

        //PLC讀回的編號
        public int MachineNumber { get; set; }

        public int ProcessState { get; set; }

        public int MachineStatus { get; set; }

        public int MissionStartedInform { get; set; }

        public int MissionCompletedInform { get; set; }

        public int AutoMode { get; set; }

        public ICollection<CakeTable> CakeTables { get; set; } = new List<CakeTable>();

        [NotMapped]
        public int CakeCount => CakeTables.Count;

        public ICollection<BobbinTable> BobbinTables { get; set; } = new List<BobbinTable>();

        [NotMapped]
        public int BobbinCount => BobbinTables.Count;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public class TwistingMachineTable
    {
        [Key]
        public Guid Id { get; set; }

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

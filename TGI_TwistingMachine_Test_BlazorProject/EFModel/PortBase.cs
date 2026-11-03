using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TGI_TwistingMachine_Test_BlazorProject.EquipName.TwistingMachine;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public abstract class PortBase
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MachineId { get; set; }

        public ESide Side { get; set; }

        public int PortNumber { get; set; }

        public int RobotNumber { get; set; }

        [MaxLength(500)]
        public string? AmrCellName { get; set; }

        public ESilkStatus SilkStatus { get; set; }

        [NotMapped]
        public bool IsOccupy => SilkStatus is not ESilkStatus.Empty;

        public bool Operating { get; set; }

        public bool Enable { get; set; }

        public virtual TwistingMachineTable? MachineTable { get; set; }
    }
}

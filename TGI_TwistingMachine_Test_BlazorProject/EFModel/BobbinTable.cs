using System.ComponentModel.DataAnnotations;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public class BobbinTable : PortBase
    {
        [MaxLength(500)]
        public string? BobbinRFID { get; set; }
    }
}

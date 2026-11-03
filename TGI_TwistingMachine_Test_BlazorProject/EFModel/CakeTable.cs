using System.ComponentModel.DataAnnotations;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public class CakeTable : PortBase
    {
        [MaxLength(500)]
        public string? CakeRFID { get; set; }
    }
}

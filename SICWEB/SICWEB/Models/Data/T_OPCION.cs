using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_OPCION
    {
        public int Opc_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(50)]
        public string Opc_c_vdesc { get; set; }
        public bool Opc_c_bestado { get; set; }
    }
}

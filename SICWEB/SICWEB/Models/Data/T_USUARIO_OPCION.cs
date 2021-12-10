using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_USUARIO_OPCION
    {
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(10)]
        public string Usua_c_cdoc_id { get; set; }
        public int Opc_c_iid { get; set; }
    }
}

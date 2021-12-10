using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PRODUCTO_PARTIDA
    {
        public int pro_partida_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string pro_partida_c_vcodigo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string pro_partida_c_vdescripcion { get; set; }
        public int isf_c_iid { get; set; }
    }
}

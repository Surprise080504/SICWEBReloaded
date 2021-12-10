using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_USUARIO_PERFIL
    {
        public byte Perf_c_yid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Required]
        [StringLength(10)]
        public string Usua_c_cdoc_id { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Usua_perfil_c_cestado { get; set; }
    }
}

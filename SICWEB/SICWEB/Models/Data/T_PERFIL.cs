using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PERFIL
    {
        public byte Perf_c_yid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Perf_c_vnomb { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string Perf_c_vdesc { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_c_cestado { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_USUARIO
    {
        [Column(TypeName = "CHAR")]
        [StringLength(10)]
        public string Usua_c_cusu_red { get; set; }
        public Nullable<bool> Usua_c_bpropietarioadministrador { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(11)]
        public string Usua_c_cidempresa { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Usua_c_cape_pat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Usua_c_cape_mat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Usua_c_cape_nombres { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(10)]
        public string Usua_c_cdoc_id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string Usua_c_vpass { get; set; }
        public bool Usua_c_bestado { get; set; }
    }
}

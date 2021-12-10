using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_DISTRITO
    {
        [Column(TypeName = "CHAR")]
        [StringLength(6)]
        public string dist_c_ccod_ubig { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string dist_c_vnomb { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(4)]
        public string prov_c_ccod { get; set; }
    }
}

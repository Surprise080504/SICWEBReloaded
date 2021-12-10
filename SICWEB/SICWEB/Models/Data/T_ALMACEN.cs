using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ALMACEN
    {
        public int alm_c_iid { get; set; }
        public bool alm_c_bactivo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string alm_c_vdesc { get; set; }
    }
}

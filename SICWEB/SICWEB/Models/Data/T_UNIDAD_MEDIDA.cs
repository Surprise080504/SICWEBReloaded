using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_UNIDAD_MEDIDA
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string und_c_vdesc { get; set; }
        public bool und_c_bactivo { get; set; }
        public Byte und_c_yid { get; set; }
    }
}

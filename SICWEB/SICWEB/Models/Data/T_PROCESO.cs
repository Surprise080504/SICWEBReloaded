using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PROCESO
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string proceso_c_vid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string proceso_c_vdescripcion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_CATEGORIA
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string categoria_c_vid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string categoria_c_vdescripcion { get; set; }
    }
}

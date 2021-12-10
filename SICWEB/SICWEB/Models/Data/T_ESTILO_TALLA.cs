using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ESTILO_TALLA
    {
        public int estilo_talla_c_iid { get; set; }
        public int estilo_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string talla_c_vid { get; set; }
    }
}

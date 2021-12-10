using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ZONA_REPARTO
    {
        public int zona_rep_c_yid { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1)]
        public string zona_rep_c_czona { get; set; }
    }
}

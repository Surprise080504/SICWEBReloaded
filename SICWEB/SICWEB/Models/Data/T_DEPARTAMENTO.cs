using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_DEPARTAMENTO
    {
        [Column(TypeName = "CHAR")]
        [StringLength(2)]
        public string depa_c_ccod { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string depa_c_vnomb { get; set; }
    }
}

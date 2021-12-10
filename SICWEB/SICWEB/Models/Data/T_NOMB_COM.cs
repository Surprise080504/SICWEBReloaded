using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_NOMB_COM
    {
        public int nomb_com_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string nomb_com_c_vnomb { get; set; }
    }
}

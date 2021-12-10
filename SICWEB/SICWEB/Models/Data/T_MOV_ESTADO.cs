using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MOV_ESTADO
    {
        public int mov_estado_iid { get; set; }
        
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string mov_estado_vdescrpcion { get; set; }
    }
}

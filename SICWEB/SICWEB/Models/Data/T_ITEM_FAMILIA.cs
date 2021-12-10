using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ITEM_FAMILIA
    {
        public int ifm_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string ifm_c_des { get; set; }
        public bool ifm_c_bactivo { get; set; }
        public Byte segmento_c_yid { get; set; }
    }
}

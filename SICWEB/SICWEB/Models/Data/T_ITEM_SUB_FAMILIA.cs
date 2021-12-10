using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ITEM_SUB_FAMILIA
    {
        public int isf_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string isf_c_vdesc { get; set; }
        public int isf_c_ifm_iid { get; set; }
        public bool isf_c_bactivo { get; set; }
    }
}

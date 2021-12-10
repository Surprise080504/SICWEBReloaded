using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ITEM_ALMACEN
    {
        public int itm_alm_c_iid { get; set; }
        public int itm_c_iid { get; set; }
        public int alm_c_iid { get; set; }
        public decimal itm_alm_c_ecantidad { get; set; }
    }
}

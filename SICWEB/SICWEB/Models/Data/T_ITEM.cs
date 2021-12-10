using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ITEM
    {
        public int itm_c_iid { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string itm_c_ccodigo { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string itm_c_vdescripcion { get; set; }
        public Decimal itm_c_dprecio_compra { get; set; }
        public Byte und_c_yid { get; set; }
        public bool itm_c_bactivo { get; set; }
        public Decimal itm_c_dprecio_venta { get; set; }
        public int pro_partida_c_iid { get; set; }
        //public DateTime Item_c_zregistro { get; set; }
        //public string Item_c_vreg_usuario { get; set; }
        //public DateTime? Item_c_zmodificacion { get; set; }
        //public string Item_c_vmod_usuario { get; set; }
    }
}

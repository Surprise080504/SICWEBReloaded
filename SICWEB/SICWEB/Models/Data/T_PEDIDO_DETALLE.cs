using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PEDIDO_DETALLE
    {
        public int ped_detalle_c_iid { get; set; }
        public int pedido_c_iid { get; set; }
        public int estilo_talla_c_iid { get; set; }
        public int ped_detalle_c_icantidad { get; set; }
        public Decimal ped_detalle_c_ecosto { get; set; }
        public Decimal ped_detalle_c_ecosto_total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MOVIMIENTO_ENTRADA_DETALLE
    {
        public int mve_det_c_iid { get; set; }
        public int mve_c_iid { get; set; }
        public decimal mve_c_ecant_pedida { get; set; }
        public decimal mve_c_ecant_recibida { get; set; }
        public string mve_c_vdescripcion_item { get; set; }
        public int mve_c_iocdet_id { get; set; }
        public int item_c_iid { get; set; }
    }
}

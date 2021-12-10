using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ORDEN_DE_COMPRA_DET
    {
        public int odc_det_c_iid { get; set; }
        public int odc_c_iid { get; set; }
        public int odc_c_iitemid { get; set; }
        public decimal odc_c_ecantidad { get; set; }
        public decimal odc_c_epreciounit { get; set; }
        public decimal odc_c_epreciototal { get; set; }
    }
}

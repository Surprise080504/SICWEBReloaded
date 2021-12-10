using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ESTILO_INSUMO
    {
        public int esti_insumo_c_iid { get; set; }
        public int estilo_c_iid { get; set; }
        public int itm_c_iid { get; set; }
        public Decimal estilo_insumo_c_ecosto { get; set; }
        public Decimal estilo_insumo_c_econsumo { get; set; }
        public Decimal estilo_insumo_c_emerma { get; set; }
        public int estilo_talla_c_iid { get; set; }
    }
}

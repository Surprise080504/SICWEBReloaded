using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MOVIMIENTO_ENTRADA
    {
        public int mve_c_iid { get; set; }
        public int odc_c_iid { get; set; }
        public DateTime mve_c_zfecharegistro { get; set; }
        public DateTime? mve_c_zguiafecha { get; set; }
        public DateTime mve_c_zfacturafecha { get; set; }
        public string mve_c_vguiacodigo { get; set; }
        public string mve_c_vfacturacodigo { get; set; }
        public int mve_c_iidalmacen { get; set; }
        public bool mve_c_bactivo { get; set; }
        public int mve_c_iestado { get; set; }
        public string mve_c_vdesestado { get; set; }
        public string mve_c_vobservacion { get; set; }
        public bool mve_c_bingresado { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ESTILO_PROCESO
    {
        public int esti_proceso_c_iid { get; set; }
        public int estilo_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string proceso_c_vid { get; set; }
        public Decimal estilo_proceso_c_ecosto { get; set; }
        public Decimal estilo_proceso_c_isegundos { get; set; }
        public int estilo_talla_c_iid { get; set; }
    }
}

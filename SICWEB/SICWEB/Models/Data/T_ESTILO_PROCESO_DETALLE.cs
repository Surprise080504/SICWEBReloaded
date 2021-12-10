using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ESTILO_PROCESO_DETALLE
    {
        public int esti_proc_detalle_c_iid { get; set; }
        public int esti_proceso_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string esti_proc_detalle_c_vdescripcion { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string esti_proc_detalle_c_vmaquina { get; set; }
        public Decimal esti_proc_detalle_c_ecosto { get; set; }
        public Decimal esti_proc_detalle_c_isegundos { get; set; }
        public Byte esti_proceso_c_yorden { get; set; }
    }
}

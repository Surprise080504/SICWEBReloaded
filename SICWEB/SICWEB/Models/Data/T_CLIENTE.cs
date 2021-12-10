using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_CLIENTE
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(11)]
        public string cli_c_vdoc_id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string cli_c_vraz_soc { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string cli_c_vpartida { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string cli_c_vrubro { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string cli_c_ctlf { get; set; }
        public DateTime? cli_c_dfec_aniv { get; set; }
        public bool cli_c_btipo_pers { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(8)]
        public string colab_c_cdoc_id { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(1)]
        public string cli_scor_c_cletra { get; set; }
        public bool cli_c_bactivo { get; set; }
        public Byte zona_rep_c_yid { get; set; }
        public DateTime cli_c_dfecharegistra { get; set; }
        public DateTime cli_c_dfechaactualiza { get; set; }
        public DateTime? cli_c_dfec_const { get; set; }
        public bool cli_c_bproveedor { get; set; }
        public bool cli_c_bcliente { get; set; }
    }
}

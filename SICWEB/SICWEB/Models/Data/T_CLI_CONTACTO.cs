using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_CLI_CONTACTO
    {
        public int cli_contac_c_iid { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(12)]
        public string cli_contac_c_cdoc_id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string cli_contac_c_vnomb { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string cli_contac_c_vape_pat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string cli_contac_c_vape_mat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string cli_contac_c_ctlf { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(15)]
        public string cli_contac_c_ccel { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string cli_contac_c_vcorreo { get; set; }
        public DateTime cli_contac_c_dfec_cump { get; set; }
        public Byte cli_contac_cargo_c_yid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string cli_contac_c_vobserv { get; set; }
        public Boolean cli_contac_c_bactivo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(11)]
        public string cli_c_vdoc_id { get; set; }
    }
}

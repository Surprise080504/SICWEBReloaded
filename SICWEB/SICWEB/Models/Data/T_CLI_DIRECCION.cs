using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_CLI_DIRECCION
    {
        public int cli_direc_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(11)]
        public string cli_c_vdoc_id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        public string cli_direc_c_vdirec { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(1)]
        public string cli_direc_c_ctipo { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(6)]
        public string dist_c_ccod_ubig { get; set; }
        [Column(TypeName = "CHAR")]
        [StringLength(1)]
        public string cli_direc_c_czonarep { get; set; }
        public bool cli_direc_c_bactivo { get; set; }
        
    }
}

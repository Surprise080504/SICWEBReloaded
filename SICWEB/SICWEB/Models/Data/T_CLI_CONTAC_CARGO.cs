using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_CLI_CONTAC_CARGO
    {
        public Byte cli_contac_cargo_c_yid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string cli_contac_cargo_c_vnomb { get; set; }
    }
}

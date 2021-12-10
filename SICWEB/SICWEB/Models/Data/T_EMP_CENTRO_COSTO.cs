using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICWEB
{
    public class T_EMP_CENTRO_COSTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int emp_cst_c_iid { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string emp_cst_c_vdesc { get; set; }

        public bool emp_cst_c_bactivo { get; set; }

        [Column(TypeName = "CHAR")]
        [StringLength(3)]
        public string emp_cst_c_vseriefactura { get; set; }

        public int emp_cst_c_inumerofact { get; set; }

        [Column(TypeName = "CHAR")]
        [StringLength(3)]
        public string emp_cst_c_vserieboleta { get; set; }

        public int emp_cst_c_inumeroboleta { get; set; }
    }
}

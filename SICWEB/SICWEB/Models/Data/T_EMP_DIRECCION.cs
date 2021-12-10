using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICWEB
{
    public class T_EMP_DIRECCION
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int emp_dir_c_iid { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string emp_dir_c_vdireccion { get; set; }

        public bool emp_dir_c_bactivo { get; set; }

        public int emp_dir_c_iid_centrocosto { get; set; }

        public int emp_dir_c_itipodirec { get; set; }

        [Column(TypeName = "CHAR")]
        [StringLength(6)]
        public string emp_dir_c_ccod_ubig { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string emp_dir_c_vtipodirec { get; set; }
    }
}

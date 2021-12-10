using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SICWEB
{
    public class T_EMPRESA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int emp_c_iid { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string emp_c_vruc { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string emp_c_vrazonsocial { get; set; }
    }
}

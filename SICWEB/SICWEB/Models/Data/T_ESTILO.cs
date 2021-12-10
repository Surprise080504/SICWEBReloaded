using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ESTILO
    {
        public int estilo_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string estilo_c_vcodigo { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string estilo_c_vnombre { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string estilo_c_vdescripcion { get; set; }
        public int itm_c_iid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string marca_c_vid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string marca_color_c_vid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string marca_categoria_c_vid { get; set; }
    }
}

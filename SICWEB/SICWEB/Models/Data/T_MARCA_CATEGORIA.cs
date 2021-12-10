using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MARCA_CATEGORIA
    {
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string marca_categoria_c_vid { get; set; }
        //[Column(TypeName = "VARCHAR")]
        //[StringLength(50)]
        //public string marca_categoria_c_vdescripcion { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Marca_categoria_c_vmaterial { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Marca_categoria_c_vproceso { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string marca_c_vid { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string categoria_c_vid { get; set; }
    }
}

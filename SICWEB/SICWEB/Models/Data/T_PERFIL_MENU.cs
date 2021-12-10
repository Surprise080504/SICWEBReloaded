using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PERFIL_MENU
    {
        public byte Perf_c_yid { get; set; }
        public int Menu_c_iid { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_calta { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_cmod { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_celim { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_cvisual { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_cimpre { get; set; }
        [Column(TypeName = "CHAR")]
        public Nullable<char> Perf_menu_c_cproc { get; set; }
    }
}

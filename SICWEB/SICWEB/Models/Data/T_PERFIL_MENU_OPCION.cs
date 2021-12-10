using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PERFIL_MENU_OPCION
    {
        public byte Perf_c_yid { get; set; }
        public int Menu_opcion_c_iid { get; set; }
        public bool Perf_menu_opcion_c_bestado { get; set; }
    }
}

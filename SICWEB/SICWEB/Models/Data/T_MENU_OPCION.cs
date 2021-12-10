using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MENU_OPCION
    {
        public int Menu_opcion_c_iid { get; set; }
        public int Menu_c_iid { get; set; }
        public int Opc_c_iid{ get; set; }
        public bool Menu_opcion_c_bestado { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_MENU
    {
        public int Menu_c_iid { get; set; }
        public int? Menu_c_iid_padre { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Menu_c_vnomb { get; set; }
        public byte? Menu_c_ynivel { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Menu_c_vpag_asp { get; set; }
        public bool Menu_c_bestado { get; set; }
    }
}

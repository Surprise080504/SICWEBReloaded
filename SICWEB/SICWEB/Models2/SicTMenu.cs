using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMenu
    {
        public SicTMenu()
        {
            InverseMenuCIidPadreNavigation = new HashSet<SicTMenu>();
            SicTPerfilMenus = new HashSet<SicTPerfilMenu>();
        }

        public int MenuCIid { get; set; }
        public int? MenuCIidPadre { get; set; }
        public string MenuCVnomb { get; set; }
        public byte? MenuCYnivel { get; set; }
        public string MenuCVpagAsp { get; set; }

        public virtual SicTMenu MenuCIidPadreNavigation { get; set; }
        public virtual ICollection<SicTMenu> InverseMenuCIidPadreNavigation { get; set; }
        public virtual ICollection<SicTPerfilMenu> SicTPerfilMenus { get; set; }
    }
}

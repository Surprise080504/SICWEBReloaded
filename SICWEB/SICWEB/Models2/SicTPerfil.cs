using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTPerfil
    {
        public SicTPerfil()
        {
            SicTPerfilMenus = new HashSet<SicTPerfilMenu>();
            SicTPerfilOpcions = new HashSet<SicTPerfilOpcion>();
        }

        public byte PerfCYid { get; set; }
        public string PerfCVnomb { get; set; }
        public string PerfCVdesc { get; set; }
        public string PerfCCestado { get; set; }

        public virtual ICollection<SicTPerfilMenu> SicTPerfilMenus { get; set; }
        public virtual ICollection<SicTPerfilOpcion> SicTPerfilOpcions { get; set; }
    }
}

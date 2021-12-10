using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTOpcion
    {
        public SicTOpcion()
        {
            SicTPerfilOpcions = new HashSet<SicTPerfilOpcion>();
            SicTUsuarioOpcions = new HashSet<SicTUsuarioOpcion>();
        }

        public int OpcCIid { get; set; }
        public string OpcCVdesc { get; set; }
        public bool OpcCBestado { get; set; }

        public virtual ICollection<SicTPerfilOpcion> SicTPerfilOpcions { get; set; }
        public virtual ICollection<SicTUsuarioOpcion> SicTUsuarioOpcions { get; set; }
    }
}

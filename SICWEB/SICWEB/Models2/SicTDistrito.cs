using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTDistrito
    {
        public SicTDistrito()
        {
            SicTCliDireccions = new HashSet<SicTCliDireccion>();
        }

        public string DistCCcodUbig { get; set; }
        public string DistCVnomb { get; set; }
        public string ProvCCcod { get; set; }

        public virtual SicTProvincium ProvCCcodNavigation { get; set; }
        public virtual ICollection<SicTCliDireccion> SicTCliDireccions { get; set; }
    }
}

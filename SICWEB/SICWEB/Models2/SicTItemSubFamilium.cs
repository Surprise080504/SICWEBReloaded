using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTItemSubFamilium
    {
        public SicTItemSubFamilium()
        {
            SicTProductoPartida = new HashSet<SicTProductoPartidum>();
        }

        public int IsfCIid { get; set; }
        public string IsfCVdesc { get; set; }
        public int IsfCIfmIid { get; set; }
        public bool IsfCBactivo { get; set; }

        public virtual SicTItemFamilium IsfCIfmI { get; set; }
        public virtual ICollection<SicTProductoPartidum> SicTProductoPartida { get; set; }
    }
}

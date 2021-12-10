using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTProductoPartidum
    {
        public SicTProductoPartidum()
        {
            SicTItems = new HashSet<SicTItem>();
        }

        public int ProPartidaCIid { get; set; }
        public string ProPartidaCVcodigo { get; set; }
        public string ProPartidaCVdescripcion { get; set; }
        public int IsfCIid { get; set; }

        public virtual SicTItemSubFamilium IsfCI { get; set; }
        public virtual ICollection<SicTItem> SicTItems { get; set; }
    }
}

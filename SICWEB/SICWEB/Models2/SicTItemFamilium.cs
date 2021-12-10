using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTItemFamilium
    {
        public SicTItemFamilium()
        {
            SicTItemSubFamilia = new HashSet<SicTItemSubFamilium>();
        }

        public int IfmCIid { get; set; }
        public string IfmCDes { get; set; }
        public bool IfmCBactivo { get; set; }
        public byte SegmentoCYid { get; set; }

        public virtual SicTSegmento SegmentoCY { get; set; }
        public virtual ICollection<SicTItemSubFamilium> SicTItemSubFamilia { get; set; }
    }
}

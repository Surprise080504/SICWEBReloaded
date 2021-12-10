using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTSegmento
    {
        public SicTSegmento()
        {
            SicTItemFamilia = new HashSet<SicTItemFamilium>();
        }

        public byte SegmentoCYid { get; set; }
        public string SegmentoCVcodigo { get; set; }
        public string SegmentoCVdescripcion { get; set; }

        public virtual ICollection<SicTItemFamilium> SicTItemFamilia { get; set; }
    }
}

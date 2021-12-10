using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTZonaRepartoLugar
    {
        public int ZonaRepLugCIid { get; set; }
        public byte? ZonaRepCYid { get; set; }
        public string ZonaRepLugCVdesc { get; set; }

        public virtual SicTZonaReparto ZonaRepCY { get; set; }
    }
}

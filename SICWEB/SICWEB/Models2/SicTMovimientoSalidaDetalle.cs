using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMovimientoSalidaDetalle
    {
        public int MvsDetCIid { get; set; }
        public int MvsCIid { get; set; }
        public int AlmCIid { get; set; }
        public int ItmCIid { get; set; }
        public decimal MvsDetCEcant { get; set; }

        public virtual SicTAlmacen AlmCI { get; set; }
        public virtual SicTItem ItmCI { get; set; }
        public virtual SicTMovimientoSalidum MvsCI { get; set; }
    }
}

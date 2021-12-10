using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTItemAlmacen
    {
        public int ItmAlmCIid { get; set; }
        public int ItmCIid { get; set; }
        public int AlmCIid { get; set; }
        public decimal ItmAlmCEcantidad { get; set; }

        public virtual SicTAlmacen AlmCI { get; set; }
        public virtual SicTItem ItmCI { get; set; }
    }
}

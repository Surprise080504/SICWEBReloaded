using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTBoletaDetalle1
    {
        public int BolDetCIid { get; set; }
        public int BolCIid { get; set; }
        public int BolDetCIitem { get; set; }
        public decimal BolDetCEcantidad { get; set; }
        public decimal BolDetCEpreciounit { get; set; }

        public virtual SicTBoletum1 BolCI { get; set; }
    }
}

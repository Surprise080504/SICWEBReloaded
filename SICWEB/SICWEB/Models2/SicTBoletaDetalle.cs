using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTBoletaDetalle
    {
        public int BolDetCIid { get; set; }
        public int BolCIid { get; set; }
        public int BolDetCIitem { get; set; }
        public decimal BolDetCEcantidad { get; set; }
        public decimal BolDetCEpreciounit { get; set; }
        public decimal BolDetCEpreciotot { get; set; }

        public virtual SicTBoletum BolCI { get; set; }
        public virtual SicTItem BolDetCIitemNavigation { get; set; }
    }
}

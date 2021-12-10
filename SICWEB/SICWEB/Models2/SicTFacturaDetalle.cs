using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTFacturaDetalle
    {
        public int FacDetCIid { get; set; }
        public int FacCIid { get; set; }
        public int FacDetCIitem { get; set; }
        public decimal FacDetCEcantidad { get; set; }
        public decimal FacDetCEpreciounit { get; set; }
        public decimal FacDetCEpreciotot { get; set; }

        public virtual SicTFactura FacCI { get; set; }
        public virtual SicTItem FacDetCIitemNavigation { get; set; }
    }
}

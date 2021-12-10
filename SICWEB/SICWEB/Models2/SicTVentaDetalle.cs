using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTVentaDetalle
    {
        public int VenDetCIid { get; set; }
        public int VenCIid { get; set; }
        public int VenDetCIitemid { get; set; }
        public decimal VenDetCEcantidad { get; set; }
        public decimal VenDetCEpreciounit { get; set; }
        public decimal VenDetCEpreciototal { get; set; }
        public int VenDetCIidalmacen { get; set; }

        public virtual SicTVentum VenCI { get; set; }
        public virtual SicTAlmacen VenDetCIidalmacenNavigation { get; set; }
        public virtual SicTItem VenDetCIitem { get; set; }
    }
}

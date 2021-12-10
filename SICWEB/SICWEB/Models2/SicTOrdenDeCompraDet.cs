using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTOrdenDeCompraDet
    {
        public SicTOrdenDeCompraDet()
        {
            SicTMovimientoEntradaDetalles = new HashSet<SicTMovimientoEntradaDetalle>();
        }

        public int OdcDetCIid { get; set; }
        public int OdcCIid { get; set; }
        public int OdcCIitemid { get; set; }
        public decimal OdcCEcantidad { get; set; }
        public decimal OdcCEpreciounit { get; set; }
        public decimal OdcCEpreciototal { get; set; }

        public virtual SicTOrdenDeCompra OdcCI { get; set; }
        public virtual SicTItem OdcCIitem { get; set; }
        public virtual ICollection<SicTMovimientoEntradaDetalle> SicTMovimientoEntradaDetalles { get; set; }
    }
}

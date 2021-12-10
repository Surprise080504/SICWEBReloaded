using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTAlmacen
    {
        public SicTAlmacen()
        {
            SicTAlmacenCentroCostos = new HashSet<SicTAlmacenCentroCosto>();
            SicTItemAlmacens = new HashSet<SicTItemAlmacen>();
            SicTMovimientoEntrada = new HashSet<SicTMovimientoEntradum>();
            SicTMovimientoSalidaDetalles = new HashSet<SicTMovimientoSalidaDetalle>();
            SicTVentaDetalles = new HashSet<SicTVentaDetalle>();
        }

        public int AlmCIid { get; set; }
        public bool? AlmCBactivo { get; set; }
        public string AlmCVdesc { get; set; }

        public virtual ICollection<SicTAlmacenCentroCosto> SicTAlmacenCentroCostos { get; set; }
        public virtual ICollection<SicTItemAlmacen> SicTItemAlmacens { get; set; }
        public virtual ICollection<SicTMovimientoEntradum> SicTMovimientoEntrada { get; set; }
        public virtual ICollection<SicTMovimientoSalidaDetalle> SicTMovimientoSalidaDetalles { get; set; }
        public virtual ICollection<SicTVentaDetalle> SicTVentaDetalles { get; set; }
    }
}

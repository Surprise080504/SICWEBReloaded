using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTItem
    {
        public SicTItem()
        {
            SicTBoletaDetalles = new HashSet<SicTBoletaDetalle>();
            SicTEstilos = new HashSet<SicTEstilo>();
            SicTFacturaDetalles = new HashSet<SicTFacturaDetalle>();
            SicTItemAlmacens = new HashSet<SicTItemAlmacen>();
            SicTMovimientoSalidaDetalles = new HashSet<SicTMovimientoSalidaDetalle>();
            SicTOrdenDeCompraDets = new HashSet<SicTOrdenDeCompraDet>();
            SicTVentaDetalles = new HashSet<SicTVentaDetalle>();
        }

        public int ItmCIid { get; set; }
        public string ItmCCcodigo { get; set; }
        public string ItmCVdescripcion { get; set; }
        public decimal ItmCDprecioCompra { get; set; }
        public byte UndCYid { get; set; }
        public bool ItmCBactivo { get; set; }
        public decimal ItmCDprecioVenta { get; set; }
        public int ProPartidaCIid { get; set; }

        public virtual SicTProductoPartidum ProPartidaCI { get; set; }
        public virtual SicTUnidadMedidum UndCY { get; set; }
        public virtual ICollection<SicTBoletaDetalle> SicTBoletaDetalles { get; set; }
        public virtual ICollection<SicTEstilo> SicTEstilos { get; set; }
        public virtual ICollection<SicTFacturaDetalle> SicTFacturaDetalles { get; set; }
        public virtual ICollection<SicTItemAlmacen> SicTItemAlmacens { get; set; }
        public virtual ICollection<SicTMovimientoSalidaDetalle> SicTMovimientoSalidaDetalles { get; set; }
        public virtual ICollection<SicTOrdenDeCompraDet> SicTOrdenDeCompraDets { get; set; }
        public virtual ICollection<SicTVentaDetalle> SicTVentaDetalles { get; set; }
    }
}

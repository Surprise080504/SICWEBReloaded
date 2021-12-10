using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTVentum
    {
        public SicTVentum()
        {
            SicTBoleta = new HashSet<SicTBoletum>();
            SicTBoletum1s = new HashSet<SicTBoletum1>();
            SicTFactura1s = new HashSet<SicTFactura1>();
            SicTFacturas = new HashSet<SicTFactura>();
            SicTMovimientoSalida = new HashSet<SicTMovimientoSalidum>();
            SicTVentaDetalles = new HashSet<SicTVentaDetalle>();
        }

        public int VenCIid { get; set; }
        public DateTime VenCZfecha { get; set; }
        public byte VenCYmoneda { get; set; }
        public decimal VenCEsubtotal { get; set; }
        public decimal VenCEtotal { get; set; }
        public string VenCVdoccliId { get; set; }
        public decimal VenCEigv { get; set; }
        public decimal VenCEigvcal { get; set; }
        public int VenCItipodoc { get; set; }
        public string VenCVdescmoneda { get; set; }
        public bool VenCBactivo { get; set; }
        public string VenCVdestipodoc { get; set; }
        public int VenCIcentrocosto { get; set; }
        public int VenCIestado { get; set; }
        public string VenCVestado { get; set; }

        public virtual SicTEmpCentroCosto VenCIcentrocostoNavigation { get; set; }
        public virtual SicTVenEstado VenCIestadoNavigation { get; set; }
        public virtual SicTCliente VenCVdoccli { get; set; }
        public virtual ICollection<SicTBoletum> SicTBoleta { get; set; }
        public virtual ICollection<SicTBoletum1> SicTBoletum1s { get; set; }
        public virtual ICollection<SicTFactura1> SicTFactura1s { get; set; }
        public virtual ICollection<SicTFactura> SicTFacturas { get; set; }
        public virtual ICollection<SicTMovimientoSalidum> SicTMovimientoSalida { get; set; }
        public virtual ICollection<SicTVentaDetalle> SicTVentaDetalles { get; set; }
    }
}

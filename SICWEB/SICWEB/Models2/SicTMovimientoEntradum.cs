using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMovimientoEntradum
    {
        public SicTMovimientoEntradum()
        {
            SicTMovimientoEntradaDetalles = new HashSet<SicTMovimientoEntradaDetalle>();
        }

        public int MveCIid { get; set; }
        public int OdcCIid { get; set; }
        public DateTime MveCZfecharegistro { get; set; }
        public DateTime? MveCZguiafecha { get; set; }
        public DateTime MveCZfacturafecha { get; set; }
        public string MveCVguiacodigo { get; set; }
        public string MveCVfacturacodigo { get; set; }
        public int MveCIidalmacen { get; set; }
        public bool MveCBactivo { get; set; }
        public int MveCIestado { get; set; }
        public string MveCVdesestado { get; set; }
        public string MveCVobservacion { get; set; }
        public bool MveCBingresado { get; set; }

        public virtual SicTMovEstado MveCIestadoNavigation { get; set; }
        public virtual SicTAlmacen MveCIidalmacenNavigation { get; set; }
        public virtual SicTOrdenDeCompra OdcCI { get; set; }
        public virtual ICollection<SicTMovimientoEntradaDetalle> SicTMovimientoEntradaDetalles { get; set; }
    }
}

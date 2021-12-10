using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMovimientoSalidum
    {
        public SicTMovimientoSalidum()
        {
            SicTMovimientoSalidaDetalles = new HashSet<SicTMovimientoSalidaDetalle>();
        }

        public int MvsCIid { get; set; }
        public int MvsCItiposalida { get; set; }
        public string MvsCVdestiposalida { get; set; }
        public DateTime MvsCZfecharegistro { get; set; }
        public bool MvsCBingresado { get; set; }
        public int? VenCIid { get; set; }
        public bool MvsCBactivo { get; set; }
        public string CliCVdocId { get; set; }
        public int MovEstadoIid { get; set; }
        public string MvsCVobservacion { get; set; }

        public virtual SicTCliente CliCVdoc { get; set; }
        public virtual SicTMovEstado MovEstadoI { get; set; }
        public virtual SicTVentum VenCI { get; set; }
        public virtual ICollection<SicTMovimientoSalidaDetalle> SicTMovimientoSalidaDetalles { get; set; }
    }
}

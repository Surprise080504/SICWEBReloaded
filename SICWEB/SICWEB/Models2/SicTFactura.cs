using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTFactura
    {
        public SicTFactura()
        {
            SicTFacturaDetalles = new HashSet<SicTFacturaDetalle>();
        }

        public int FacCIid { get; set; }
        public DateTime FacCZfecharegistro { get; set; }
        public string FacCSerie { get; set; }
        public int FacCNumero { get; set; }
        public int FacCIventa { get; set; }
        public decimal FacCEigv { get; set; }
        public decimal FacCEigvcal { get; set; }
        public decimal FacCEsubtotal { get; set; }
        public decimal FacCEtotal { get; set; }
        public int FacCImoneda { get; set; }
        public string FacCVdescmoneda { get; set; }
        public bool FacCBimpreso { get; set; }

        public virtual SicTVentum FacCIventaNavigation { get; set; }
        public virtual ICollection<SicTFacturaDetalle> SicTFacturaDetalles { get; set; }
    }
}

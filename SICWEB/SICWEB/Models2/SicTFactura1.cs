using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTFactura1
    {
        public SicTFactura1()
        {
            SicTFacturaDetalle1s = new HashSet<SicTFacturaDetalle1>();
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

        public virtual SicTVentum FacCIventaNavigation { get; set; }
        public virtual ICollection<SicTFacturaDetalle1> SicTFacturaDetalle1s { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTBoletum1
    {
        public SicTBoletum1()
        {
            SicTBoletaDetalle1s = new HashSet<SicTBoletaDetalle1>();
        }

        public int BolCIid { get; set; }
        public DateTime BolCZfecharegistro { get; set; }
        public string BolCSerie { get; set; }
        public int BolCNumero { get; set; }
        public int BolCIventa { get; set; }
        public decimal BolCEigv { get; set; }
        public decimal BolCEigvcal { get; set; }
        public decimal BolCEsubtotal { get; set; }
        public decimal BolCEtotal { get; set; }
        public int BolCImoneda { get; set; }
        public string BolCVdescmoneda { get; set; }

        public virtual SicTVentum BolCIventaNavigation { get; set; }
        public virtual ICollection<SicTBoletaDetalle1> SicTBoletaDetalle1s { get; set; }
    }
}

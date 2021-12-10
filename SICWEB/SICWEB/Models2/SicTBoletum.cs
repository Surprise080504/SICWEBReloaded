using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTBoletum
    {
        public SicTBoletum()
        {
            SicTBoletaDetalles = new HashSet<SicTBoletaDetalle>();
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
        public bool BolCBimpreso { get; set; }

        public virtual SicTVentum BolCIventaNavigation { get; set; }
        public virtual ICollection<SicTBoletaDetalle> SicTBoletaDetalles { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMovimientoEntradaDetalle
    {
        public int MveDetCIid { get; set; }
        public int MveCIid { get; set; }
        public decimal MveCEcantPedida { get; set; }
        public decimal MveCEcantRecibida { get; set; }
        public string MveCVdescripcionItem { get; set; }
        public int MveCIocdetId { get; set; }

        public virtual SicTMovimientoEntradum MveCI { get; set; }
        public virtual SicTOrdenDeCompraDet MveCIocdet { get; set; }
    }
}

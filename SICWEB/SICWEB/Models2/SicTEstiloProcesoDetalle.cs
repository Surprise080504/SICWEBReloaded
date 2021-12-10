using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTEstiloProcesoDetalle
    {
        public int EstiProcDetalleCIid { get; set; }
        public int EstiProcesoCIid { get; set; }
        public string EstiProcDetalleCVdescripcion { get; set; }
        public string EstiProcDetalleCVmaquina { get; set; }
        public decimal EstiProcDetalleCEcosto { get; set; }
        public decimal EstiProcDetalleCIsegundos { get; set; }
        public byte EstiProcesoCYorden { get; set; }

        public virtual SicTEstiloProceso EstiProcesoCI { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTEstiloProceso
    {
        public SicTEstiloProceso()
        {
            SicTEstiloProcesoDetalles = new HashSet<SicTEstiloProcesoDetalle>();
        }

        public int EstiProcesoCIid { get; set; }
        public int EstiCIid { get; set; }
        public string ProcCVid { get; set; }
        public decimal EstiProcesoCEcosto { get; set; }

        public virtual SicTEstilo EstiCI { get; set; }
        public virtual SicTProceso ProcCV { get; set; }
        public virtual ICollection<SicTEstiloProcesoDetalle> SicTEstiloProcesoDetalles { get; set; }
    }
}

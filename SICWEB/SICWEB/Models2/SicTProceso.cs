using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTProceso
    {
        public SicTProceso()
        {
            SicTEstiloProcesos = new HashSet<SicTEstiloProceso>();
        }

        public string ProcCVid { get; set; }
        public string ProcCVdescripcion { get; set; }

        public virtual ICollection<SicTEstiloProceso> SicTEstiloProcesos { get; set; }
    }
}

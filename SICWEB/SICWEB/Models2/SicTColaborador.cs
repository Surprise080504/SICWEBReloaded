using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTColaborador
    {
        public SicTColaborador()
        {
            SicTClientes = new HashSet<SicTCliente>();
        }

        public string ColabCCdocId { get; set; }
        public string ColabCCusuRed { get; set; }
        public string ColabCVnomb { get; set; }
        public string ColabCVapePat { get; set; }
        public string ColabCVapeMat { get; set; }
        public byte? ColabAreaCYid { get; set; }
        public byte? ColabCargoCYid { get; set; }

        public virtual SicTColabArea ColabAreaCY { get; set; }
        public virtual SicTColabCargo ColabCargoCY { get; set; }
        public virtual ICollection<SicTCliente> SicTClientes { get; set; }
    }
}

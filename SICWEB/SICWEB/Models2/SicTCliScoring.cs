using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliScoring
    {
        public SicTCliScoring()
        {
            SicTClientes = new HashSet<SicTCliente>();
        }

        public string CliScorCCletra { get; set; }
        public string CliScorCVobserv { get; set; }

        public virtual ICollection<SicTCliente> SicTClientes { get; set; }
    }
}

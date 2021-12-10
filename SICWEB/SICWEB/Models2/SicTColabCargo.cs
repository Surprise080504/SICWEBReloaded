using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTColabCargo
    {
        public SicTColabCargo()
        {
            SicTColaboradors = new HashSet<SicTColaborador>();
        }

        public byte ColabCargoCYid { get; set; }
        public string ColabCargoCVnomb { get; set; }

        public virtual ICollection<SicTColaborador> SicTColaboradors { get; set; }
    }
}

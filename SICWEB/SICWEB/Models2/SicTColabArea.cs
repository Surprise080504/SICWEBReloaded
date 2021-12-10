using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTColabArea
    {
        public SicTColabArea()
        {
            SicTColaboradors = new HashSet<SicTColaborador>();
        }

        public byte ColabAreaCYid { get; set; }
        public string ColabAreaCVnomb { get; set; }

        public virtual ICollection<SicTColaborador> SicTColaboradors { get; set; }
    }
}

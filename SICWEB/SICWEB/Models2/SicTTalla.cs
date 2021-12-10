using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTTalla
    {
        public SicTTalla()
        {
            SicTEstilos = new HashSet<SicTEstilo>();
        }

        public string TallaCVid { get; set; }
        public string TallaCVcodigo { get; set; }
        public string TallaCVdescripcion { get; set; }

        public virtual ICollection<SicTEstilo> SicTEstilos { get; set; }
    }
}

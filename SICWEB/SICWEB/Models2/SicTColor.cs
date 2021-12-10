using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTColor
    {
        public SicTColor()
        {
            SicTEstilos = new HashSet<SicTEstilo>();
        }

        public string ColorCVid { get; set; }
        public string ColorCVcodigo { get; set; }
        public string ColorCVdescripcion { get; set; }

        public virtual ICollection<SicTEstilo> SicTEstilos { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCategorium
    {
        public SicTCategorium()
        {
            SicTEstilos = new HashSet<SicTEstilo>();
        }

        public string CateCVid { get; set; }
        public string CateCVcodigo { get; set; }
        public string CateCVdescripcion { get; set; }

        public virtual ICollection<SicTEstilo> SicTEstilos { get; set; }
    }
}

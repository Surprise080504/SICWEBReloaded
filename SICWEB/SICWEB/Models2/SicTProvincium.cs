using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTProvincium
    {
        public SicTProvincium()
        {
            SicTDistritos = new HashSet<SicTDistrito>();
        }

        public string ProvCCcod { get; set; }
        public string ProvCVnomb { get; set; }
        public string DepaCCcod { get; set; }

        public virtual SicTDepartamento DepaCCcodNavigation { get; set; }
        public virtual ICollection<SicTDistrito> SicTDistritos { get; set; }
    }
}

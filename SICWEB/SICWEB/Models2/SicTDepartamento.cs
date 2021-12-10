using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTDepartamento
    {
        public SicTDepartamento()
        {
            SicTProvincia = new HashSet<SicTProvincium>();
        }

        public string DepaCCcod { get; set; }
        public string DepaCVnomb { get; set; }

        public virtual ICollection<SicTProvincium> SicTProvincia { get; set; }
    }
}

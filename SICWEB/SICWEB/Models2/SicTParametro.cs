using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTParametro
    {
        public SicTParametro()
        {
            SicTParametroDets = new HashSet<SicTParametroDet>();
        }

        public int ParCIid { get; set; }
        public string ParCVdesc { get; set; }
        public bool? ParCBactivo { get; set; }

        public virtual ICollection<SicTParametroDet> SicTParametroDets { get; set; }
    }
}

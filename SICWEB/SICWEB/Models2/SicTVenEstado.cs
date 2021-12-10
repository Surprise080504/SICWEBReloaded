using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTVenEstado
    {
        public SicTVenEstado()
        {
            SicTVenta = new HashSet<SicTVentum>();
        }

        public int VenEstCIid { get; set; }
        public string VenEstCVdescripcion { get; set; }

        public virtual ICollection<SicTVentum> SicTVenta { get; set; }
    }
}

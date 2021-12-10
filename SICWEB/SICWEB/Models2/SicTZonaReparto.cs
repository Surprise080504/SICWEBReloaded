using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTZonaReparto
    {
        public SicTZonaReparto()
        {
            SicTClientes = new HashSet<SicTCliente>();
            SicTZonaRepartoLugars = new HashSet<SicTZonaRepartoLugar>();
        }

        public byte ZonaRepCYid { get; set; }
        public string ZonaRepCCzona { get; set; }

        public virtual ICollection<SicTCliente> SicTClientes { get; set; }
        public virtual ICollection<SicTZonaRepartoLugar> SicTZonaRepartoLugars { get; set; }
    }
}

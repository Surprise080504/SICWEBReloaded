using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliContacCargo
    {
        public SicTCliContacCargo()
        {
            SicTCliContactos = new HashSet<SicTCliContacto>();
        }

        public byte CliContacCargoCYid { get; set; }
        public string CliContacCargoCVnomb { get; set; }

        public virtual ICollection<SicTCliContacto> SicTCliContactos { get; set; }
    }
}

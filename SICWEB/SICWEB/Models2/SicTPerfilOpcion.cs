using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTPerfilOpcion
    {
        public byte PerfCYid { get; set; }
        public int OpcCIid { get; set; }

        public virtual SicTOpcion OpcCI { get; set; }
        public virtual SicTPerfil PerfCY { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTPerfilMenu
    {
        public byte PerfCYid { get; set; }
        public int MenuCIid { get; set; }
        public string PerfMenuCCalta { get; set; }
        public string PerfMenuCCmod { get; set; }
        public string PerfMenuCCelim { get; set; }
        public string PerfMenuCCvisual { get; set; }
        public string PerfMenuCCimpre { get; set; }
        public string PerfMenuCCproc { get; set; }

        public virtual SicTMenu MenuCI { get; set; }
        public virtual SicTPerfil PerfCY { get; set; }
    }
}

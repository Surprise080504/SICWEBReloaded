using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTUnidadMedidum
    {
        public SicTUnidadMedidum()
        {
            SicTItems = new HashSet<SicTItem>();
        }

        public byte UndCYid { get; set; }
        public string UndCVdesc { get; set; }
        public bool UndCBactivo { get; set; }

        public virtual ICollection<SicTItem> SicTItems { get; set; }
    }
}

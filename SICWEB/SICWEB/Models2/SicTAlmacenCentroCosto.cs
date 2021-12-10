using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTAlmacenCentroCosto
    {
        public int AlmCstCIid { get; set; }
        public int AlmCstCIidCentroCosto { get; set; }
        public int AlmCstCIidAlmacen { get; set; }

        public virtual SicTAlmacen AlmCstCIidAlmacenNavigation { get; set; }
        public virtual SicTEmpCentroCosto AlmCstCIidCentroCostoNavigation { get; set; }
    }
}

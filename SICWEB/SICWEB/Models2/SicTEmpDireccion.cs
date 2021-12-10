using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTEmpDireccion
    {
        public SicTEmpDireccion()
        {
            SicTOrdenDeCompras = new HashSet<SicTOrdenDeCompra>();
        }

        public int EmpDirCIid { get; set; }
        public string EmpDirCVdireccion { get; set; }
        public bool EmpDirCBactivo { get; set; }
        public int EmpDirCIidCentrocosto { get; set; }
        public int EmpDirCItipodirec { get; set; }
        public string EmpDirCCcodUbig { get; set; }
        public string EmpDirCVtipodirec { get; set; }

        public virtual SicTEmpCentroCosto EmpDirCIidCentrocostoNavigation { get; set; }
        public virtual ICollection<SicTOrdenDeCompra> SicTOrdenDeCompras { get; set; }
    }
}

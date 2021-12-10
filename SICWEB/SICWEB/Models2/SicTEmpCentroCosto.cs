using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTEmpCentroCosto
    {
        public SicTEmpCentroCosto()
        {
            SicTAlmacenCentroCostos = new HashSet<SicTAlmacenCentroCosto>();
            SicTEmpDireccions = new HashSet<SicTEmpDireccion>();
            SicTVenta = new HashSet<SicTVentum>();
        }

        public int EmpCstCIid { get; set; }
        public string EmpCstCVdesc { get; set; }
        public bool EmpCstCBactivo { get; set; }
        public string EmpCstCVseriefactura { get; set; }
        public int EmpCstCInumerofact { get; set; }
        public string EmpCstCVserieboleta { get; set; }
        public int EmpCstCInumeroboleta { get; set; }

        public virtual ICollection<SicTAlmacenCentroCosto> SicTAlmacenCentroCostos { get; set; }
        public virtual ICollection<SicTEmpDireccion> SicTEmpDireccions { get; set; }
        public virtual ICollection<SicTVentum> SicTVenta { get; set; }
    }
}

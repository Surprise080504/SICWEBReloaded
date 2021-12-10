using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTOdcClase
    {
        public SicTOdcClase()
        {
            SicTOrdenDeCompras = new HashSet<SicTOrdenDeCompra>();
        }

        public int OdcClaIid { get; set; }
        public string OdcClaVdes { get; set; }

        public virtual ICollection<SicTOrdenDeCompra> SicTOrdenDeCompras { get; set; }
    }
}

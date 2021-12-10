using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTOdcEstado
    {
        public SicTOdcEstado()
        {
            SicTOrdenDeCompras = new HashSet<SicTOrdenDeCompra>();
        }

        public int OdcEstadoIid { get; set; }
        public string OdcEstadoVdescripcion { get; set; }

        public virtual ICollection<SicTOrdenDeCompra> SicTOrdenDeCompras { get; set; }
    }
}

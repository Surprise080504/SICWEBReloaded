using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTMovEstado
    {
        public SicTMovEstado()
        {
            SicTMovimientoEntrada = new HashSet<SicTMovimientoEntradum>();
            SicTMovimientoSalida = new HashSet<SicTMovimientoSalidum>();
        }

        public int MovEstadoIid { get; set; }
        public string MovEstadoVdescrpcion { get; set; }

        public virtual ICollection<SicTMovimientoEntradum> SicTMovimientoEntrada { get; set; }
        public virtual ICollection<SicTMovimientoSalidum> SicTMovimientoSalida { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliente
    {
        public SicTCliente()
        {
            SicTCliContactos = new HashSet<SicTCliContacto>();
            SicTCliDireccions = new HashSet<SicTCliDireccion>();
            SicTCliNombComs = new HashSet<SicTCliNombCom>();
            SicTMovimientoSalida = new HashSet<SicTMovimientoSalidum>();
            SicTOrdenDeCompras = new HashSet<SicTOrdenDeCompra>();
            SicTVenta = new HashSet<SicTVentum>();
        }

        public string CliCVrazSoc { get; set; }
        public string CliCVpartida { get; set; }
        public string CliCVrubro { get; set; }
        public string CliCCtlf { get; set; }
        public DateTime? CliCDfecAniv { get; set; }
        public bool? CliCBtipoPers { get; set; }
        public string ColabCCdocId { get; set; }
        public string CliScorCCletra { get; set; }
        public bool? CliCBactivo { get; set; }
        public string CliCVdocId { get; set; }
        public byte? ZonaRepCYid { get; set; }
        public DateTime CliCDfecharegistra { get; set; }
        public DateTime? CliCDfechaactualiza { get; set; }
        public DateTime? CliCDfecConst { get; set; }
        public bool? CliCBproveedor { get; set; }
        public bool? CliCBcliente { get; set; }

        public virtual SicTCliScoring CliScorCCletraNavigation { get; set; }
        public virtual SicTColaborador ColabCCdoc { get; set; }
        public virtual SicTZonaReparto ZonaRepCY { get; set; }
        public virtual ICollection<SicTCliContacto> SicTCliContactos { get; set; }
        public virtual ICollection<SicTCliDireccion> SicTCliDireccions { get; set; }
        public virtual ICollection<SicTCliNombCom> SicTCliNombComs { get; set; }
        public virtual ICollection<SicTMovimientoSalidum> SicTMovimientoSalida { get; set; }
        public virtual ICollection<SicTOrdenDeCompra> SicTOrdenDeCompras { get; set; }
        public virtual ICollection<SicTVentum> SicTVenta { get; set; }
    }
}

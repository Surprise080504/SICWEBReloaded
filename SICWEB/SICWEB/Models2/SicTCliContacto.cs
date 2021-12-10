using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliContacto
    {
        public int CliContacCIid { get; set; }
        public string CliContacCCdocId { get; set; }
        public string CliContacCVnomb { get; set; }
        public string CliContacCVapePat { get; set; }
        public string CliContacCVapeMat { get; set; }
        public string CliContacCCtlf { get; set; }
        public string CliContacCCcel { get; set; }
        public string CliContacCVcorreo { get; set; }
        public DateTime? CliContacCDfecCump { get; set; }
        public byte CliContacCargoCYid { get; set; }
        public string CliContacCVobserv { get; set; }
        public bool? CliContacCBactivo { get; set; }
        public string CliCVdocId { get; set; }

        public virtual SicTCliente CliCVdoc { get; set; }
        public virtual SicTCliContacCargo CliContacCargoCY { get; set; }
    }
}

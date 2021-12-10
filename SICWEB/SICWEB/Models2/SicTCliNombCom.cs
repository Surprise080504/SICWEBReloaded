using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliNombCom
    {
        public int NombComCIid { get; set; }
        public string CliCVdocId { get; set; }

        public virtual SicTCliente CliCVdoc { get; set; }
    }
}

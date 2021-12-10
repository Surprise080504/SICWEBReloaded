using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTCliDireccion
    {
        public int CliDirecCIid { get; set; }
        public string CliDirecCVdirec { get; set; }
        public string CliDirecCCtipo { get; set; }
        public string DistCCcodUbig { get; set; }
        public bool? CliDirecCBactivo { get; set; }
        public string CliCVdocId { get; set; }
        public string CliDirecCCzonarep { get; set; }

        public virtual SicTCliente CliCVdoc { get; set; }
        public virtual SicTDistrito DistCCcodUbigNavigation { get; set; }
    }
}

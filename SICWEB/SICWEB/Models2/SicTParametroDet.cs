using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTParametroDet
    {
        public int ParCIid { get; set; }
        public int ParDetCIid { get; set; }
        public string ParDetCVdesc { get; set; }
        public string ParDetCVcampo1 { get; set; }
        public string ParDetCVcampoDesc1 { get; set; }
        public string ParDetCVcampo2 { get; set; }
        public string ParDetCVcampoDesc2 { get; set; }
        public string ParDetCVcampo3 { get; set; }
        public string ParDetCVcampoDesc3 { get; set; }
        public string ParDetCVcampo4 { get; set; }
        public string ParDetCVcampoDesc4 { get; set; }
        public string ParDetCVcampo5 { get; set; }
        public string ParDetCVcampoDesc5 { get; set; }
        public string ParDetCVobs { get; set; }

        public virtual SicTParametro ParCI { get; set; }
    }
}

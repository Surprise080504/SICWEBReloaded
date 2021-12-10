using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTIgv
    {
        public int IgvCIid { get; set; }
        public decimal? IgvCEigv { get; set; }
        public DateTime? IgvCDinicio { get; set; }
        public DateTime? IgvCDfin { get; set; }
    }
}

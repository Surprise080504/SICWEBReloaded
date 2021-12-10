using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTTasaCambio
    {
        public int TscCIid { get; set; }
        public decimal TscCEcompra { get; set; }
        public decimal TscCEventa { get; set; }
        public DateTime TscCDinicio { get; set; }
        public DateTime? TscCDfin { get; set; }
    }
}

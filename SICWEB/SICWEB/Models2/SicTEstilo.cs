using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTEstilo
    {
        public SicTEstilo()
        {
            SicTEstiloProcesos = new HashSet<SicTEstiloProceso>();
        }

        public int EstiCIid { get; set; }
        public string EstiCVcodigo { get; set; }
        public string EstiCVnombre { get; set; }
        public string EstiCVdescripcion { get; set; }
        public int ItmCIid { get; set; }
        public string ColorCVid { get; set; }
        public string CateCVid { get; set; }
        public string TallaCVid { get; set; }

        public virtual SicTCategorium CateCV { get; set; }
        public virtual SicTColor ColorCV { get; set; }
        public virtual SicTItem ItmCI { get; set; }
        public virtual SicTTalla TallaCV { get; set; }
        public virtual ICollection<SicTEstiloProceso> SicTEstiloProcesos { get; set; }
    }
}

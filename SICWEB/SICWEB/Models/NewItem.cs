using System;

namespace SICWEB.Models
{
    public class NewItem
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Decimal purchaseprice { get; set; }
        public Byte unit { get; set; }
        public bool itm_c_bactivo { get; set; }
        public Decimal saleprice { get; set; }
        public int pid { get; set; }

        public int family { get; set; }

        public int subfamily { get; set; }
    }
}
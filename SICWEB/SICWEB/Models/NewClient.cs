using System;

namespace SICWEB.Models
{
    public class NewClient
    {
        public int id { get; set; }
        public DateTime? anniversary { get; set; }
        public string company { get; set; }
        public DateTime? constitution { get; set; }
        public Byte delivery { get; set; }
        public string? ditem { get; set; }
        public int person { get; set; }
        public string phone { get; set; }
        public string ruc { get; set; }
        public string? snumber { get; set; }
        public bool provider { get; set; }
        public bool client { get; set; }
        public Trade[] trade { get; set; }
        public NewDirection[] direction { get; set; }
        public NewContact[] contact { get; set; }
    }

    public class Trade
    { 
        public int id { get; set; }
        public string value { get; set; }
    }
}
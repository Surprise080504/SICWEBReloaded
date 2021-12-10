using System;
using System.Collections.Generic;

namespace SICWEB.Models
{
    public class NewProfile
    {
        public int id { get; set; }
        public string profile { get; set; }
        public string description { get; set; }
        public int estado { get; set; }
        public int method { get; set; }
        public List<string> checkvalues { get; set; }
    }
}
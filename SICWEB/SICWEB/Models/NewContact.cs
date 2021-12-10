using System;

namespace SICWEB.Models
{
    public class NewContact
    {
        public int id { get; set; }
        public DateTime? birthday { get; set; }
        public string email { get; set; }
        public int cargo { get; set; }
        public int type { get; set; }

        public string identification { get; set; }
        public string landline { get; set; }

        public string name { get; set; }
        public string lastname { get; set; }
        public string surname { get; set; }
        public string observations { get; set; }
        public string phone { get; set; }

    }
}
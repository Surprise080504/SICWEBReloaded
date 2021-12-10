using System;
using System.Collections.Generic;

namespace SICWEB.Models
{
    public class NewMenu
    {
        public int id { get; set; }
        public int? parent_id { get; set; }
        public string menu { get; set; }
        public byte? nivel { get; set; }
        public string pagina { get; set; }
        public bool estado { get; set; }
        public List<NewOpcs> opciones { get; set; }
    }

    public class NewOpcs
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
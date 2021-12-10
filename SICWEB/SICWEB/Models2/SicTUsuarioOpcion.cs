using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTUsuarioOpcion
    {
        public string UsuaCCdocId { get; set; }
        public int OpcCIid { get; set; }

        public virtual SicTOpcion OpcCI { get; set; }
        public virtual SicTUsuario UsuaCCdoc { get; set; }
    }
}

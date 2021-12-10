using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTUsuario
    {
        public SicTUsuario()
        {
            SicTUsuarioOpcions = new HashSet<SicTUsuarioOpcion>();
        }

        public string UsuaCCusuRed { get; set; }
        public bool? UsuaCBpropietarioadministrador { get; set; }
        public string UsuaCCidempresa { get; set; }
        public string UsuaCCapePat { get; set; }
        public string UsuaCCapeMat { get; set; }
        public string UsuaCCapeNombres { get; set; }
        public string UsuaCCdocId { get; set; }
        public string UsuaCVpass { get; set; }
        public bool UsuaCBestado { get; set; }

        public virtual ICollection<SicTUsuarioOpcion> SicTUsuarioOpcions { get; set; }
    }
}

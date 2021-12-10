using System;
using System.Collections.Generic;

#nullable disable

namespace SICWEB.Models2
{
    public partial class SicTOrdenDeCompra
    {
        public SicTOrdenDeCompra()
        {
            SicTMovimientoEntrada = new HashSet<SicTMovimientoEntradum>();
            SicTOrdenDeCompraDets = new HashSet<SicTOrdenDeCompraDet>();
        }

        public int OdcCIid { get; set; }
        public string OdcCVcodigo { get; set; }
        public DateTime OdcCZfecharegistro { get; set; }
        public byte OdcCYmoneda { get; set; }
        public decimal OdcCEsubtotal { get; set; }
        public decimal OdcCEtotal { get; set; }
        public decimal OdcCEigv { get; set; }
        public decimal OdcCEigvcal { get; set; }
        public decimal OdcCEpercepcion { get; set; }
        public decimal OdcCEpercepcioncal { get; set; }
        public int OdcCIestado { get; set; }
        public string OdcCVdescmoneda { get; set; }
        public bool OdcCBactivo { get; set; }
        public string OdcCVdescestado { get; set; }
        public string ProvCVdocId { get; set; }
        public DateTime OdcCZfechaentregaIni { get; set; }
        public DateTime OdcCZfechaentregaFin { get; set; }
        public string OdcCIidUsuarioCreador { get; set; }
        public string OdcCIidUsuarioMod { get; set; }
        public DateTime? OdcCZfecharmod { get; set; }
        public string OdcCVobservacion { get; set; }
        public int OdcCClaseIid { get; set; }
        public string OdcCClaseDes { get; set; }
        public bool OdcCBpercepcion { get; set; }
        public int EmpDirCIid { get; set; }
        public string OdcCCserie { get; set; }
        public DateTime OdcCZfechaemi { get; set; }

        public virtual SicTEmpDireccion EmpDirCI { get; set; }
        public virtual SicTOdcClase OdcCClaseI { get; set; }
        public virtual SicTOdcEstado OdcCIestadoNavigation { get; set; }
        public virtual SicTCliente ProvCVdoc { get; set; }
        public virtual ICollection<SicTMovimientoEntradum> SicTMovimientoEntrada { get; set; }
        public virtual ICollection<SicTOrdenDeCompraDet> SicTOrdenDeCompraDets { get; set; }
    }
}

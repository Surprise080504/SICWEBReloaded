using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_ORDEN_DE_COMPRA
    {
        public int odc_c_iid { get; set; }
        public string odc_c_vcodigo { get; set; }
        public DateTime odc_c_zfecharegistro { get; set; }
        public byte odc_c_ymoneda { get; set; }
        public decimal odc_c_esubtotal { get; set; }
        public decimal odc_c_etotal { get; set; }
        public decimal odc_c_eigv { get; set; }
        public decimal odc_c_eigvcal { get; set; }
        public decimal odc_c_epercepcion { get; set; }
        public decimal odc_c_epercepcioncal { get; set; }
        public int odc_c_iestado { get; set; }
        public string odc_c_vdescmoneda { get; set; }
        public bool odc_c_bactivo { get; set; }
        public string odc_c_vdescestado { get; set; }
        public string prov_c_vdoc_id { get; set; }
        public DateTime odc_c_zfechaentrega_ini { get; set; }
        public DateTime odc_c_zfechaentrega_fin { get; set; }
        public string odc_c_iid_usuario_creador { get; set; }
        public string odc_c_iid_usuario_mod { get; set; }
        public DateTime? odc_c_zfecharmod { get; set; }
        public string odc_c_vobservacion { get; set; }
        public int odc_c_clase_iid { get; set; }
        public string odc_c_clase_des { get; set; }
        public bool odc_c_bpercepcion { get; set; }
        public int emp_dir_c_iid { get; set; }
        public string odc_c_cserie { get; set; }
        public DateTime odc_c_zfechaemi { get; set; }
    }
}

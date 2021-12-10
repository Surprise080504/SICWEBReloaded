using System;

namespace SICWEB.Models
{
    public class NewOrden
    {
        public int id { get; set; }
        public int clase { get; set; }
        public string codigo { get; set; }
        public int dlvaddr { get; set; }
        public DateTime entregaend { get; set; }
        public DateTime entregastart { get; set; }
        public int estado { get; set; }
        public DateTime issuedate { get; set; }
        public OrdenItem[] items { get; set; }
        public int moneda { get; set; }
        public string observe { get; set; }
        public bool percentcheck { get; set; }
        public string proveedor { get; set; }
        public string ruc_proveedor { get; set; }
        public string serie { get; set; }
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public decimal igvcal { get; set; }
        public decimal perceptioncal { get; set; }
        public string vdescestado { get; set; }
        public string vdescmoneda { get; set; }
    }

    public class OrdenItem
    {
        public bool checkstate { get; set; }
        public string itm_c_ccodigo { get; set; }
        public string itm_c_vdescripcion { get; set; }
        public decimal quantity { get; set; }
        public decimal und_c_yid { get; set; }
        public int odc_c_iid { get; set; }
        public int itm_c_iid { get; set; }
        public int odc_det_c_iid { get; set; }
    }
}
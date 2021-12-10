using System;

namespace SICWEB.Models
{
    public class NewEntrada
    {
        public int mve_c_iid { get; set; }
        public int odc_c_iid { get; set; }
        public DateTime? mve_c_zguiafecha { get; set; }
        public DateTime mve_c_zfacturafecha { get; set; }
        public string mve_c_vguiacodigo { get; set; }
        public string mve_c_vfacturacodigo { get; set; }
        public int mve_c_iidalmacen { get; set; }
        public int mve_c_iestado { get; set; }
        public string mve_c_vdesestado { get; set; }
        public string mve_c_vobservacion { get; set; }
        public EntradaItem[] items { get; set; }
    }

    public class EntradaItem
    {
        public int iocdet_id { get; set; }
        public int item_c_iid { get; set; }
        public decimal pedida { get; set; }
        public decimal atendida { get; set; }
        public decimal recibida { get; set; }
        public string descripcion { get; set; }
    }
}
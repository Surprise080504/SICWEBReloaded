using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SICWEB.Models
{    

    public class NewInsumo
    {
        public int id { get; set; }
        public int estilo_c_iid { get; set; }
        public int estilo_talla_c_iid { get; set; }
        public EstiloInsumo[] estiloInsumoses{ get; set; }
        public SizeArray[] sizes { get; set; }
        public Estilo estilo { get; set; } 
        public int isReplicate { get; set; }

    }
        

    public class EstiloInsumo
    {
        public int esti_insumo_c_iid { get; set; }
        public int estilo_c_iid { get; set; }
        public int itm_c_iid { get; set; }
        public Decimal estilo_insumo_c_ecosto { get; set; }
        public Decimal estilo_insumo_c_econsumo { get; set; }
        public Decimal estilo_insumo_c_emerma { get; set; }
    }


}
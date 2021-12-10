using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SICWEB.Models
{    

    public class NewProcess
    {
        public int id { get; set; }
        public int estilo_c_iid { get; set; }
        public int estilo_talla_c_iid { get; set; }
        public EstiloProcess[] estiloProcesses { get; set; }
        public SizeArray[] sizes { get; set; }
        public Estilo estilo { get; set; } 

        public int isReplicate { get; set; }

    }

    public class Estilo
    {
        public int estilo_c_iid { get; set; }
        public string estilo_c_vcodigo { get; set; }
        public string estilo_c_vnombre { get; set; }
        public string estilo_c_vdescripcion { get; set; }
        public int itm_c_iid { get; set; }
        public string marca_c_vid { get; set; }
        public string marca_categoria_c_vid { get; set; }
        public string marca_color_c_vid { get; set; }
    }

    public class EstiloProcessResult
    {
        public int estilo_c_iid { get; set; }
        public int esti_proceso_c_iid { get; set; }
        public string  proceso_c_vid { get; set; }
        public string esti_proc_detalle_c_vdescripcion { get; set; }
        public decimal esti_proc_detalle_c_ecosto { get; set; }
        public decimal esti_proc_detalle_c_isegundos { get; set; }
        public int esti_proc_detalle_c_iid { get; set; }
        public int estilo_talla_c_iid { get; set; }
        public int esti_proceso_c_yorden { get; set; }
    }

    public class EstiloProcess
    {
        public int esti_proceso_c_iid { get; set; }
        public string proceso_c_vid { get; set; }
        public string esti_proc_detalle_c_vdescripcion { get; set; }
        public Decimal esti_proc_detalle_c_ecosto { get; set; }
        public Decimal esti_proc_detalle_c_isegundos { get; set; }
        public childProcess[] childinfo { get; set; }
    }

    public class childProcess
    {
        public int childId { get; set; }
        public string vdesc { get; set; }
        public int cost { get; set; }
        public int effot { get; set; }
    }

}
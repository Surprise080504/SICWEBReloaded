using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SICWEB
{
    public class T_PARAMETRO_DET
    {
        public int par_c_iid { get; set; }
        public int par_det_c_iid { get; set; }
        public string par_det_c_vdesc { get; set; }
        public string par_det_c_vcampo_1 { get; set; }
        public string par_det_c_vcampo_desc_1 { get; set; }
        public string par_det_c_vcampo_2 { get; set; }
        public string par_det_c_vcampo_desc_2 { get; set; }
        public string par_det_c_vcampo_3 { get; set; }
        public string par_det_c_vcampo_desc_3 { get; set; }
        public string par_det_c_vcampo_4 { get; set; }
        public string par_det_c_vcampo_desc_4 { get; set; }
        public string par_det_c_vcampo_5 { get; set; }
        public string par_det_c_vcampo_desc_5 { get; set; }
        public string par_det_c_vobs { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SICWEB.Models
{    

    public class NewStyle
    {
        public int id { get; set; }
        public string code { get; set; }
        public string brand { get; set; }
        public string category { get; set; }
        public string color { get; set; }
        public string description { get; set; }
        public int item { get; set; }
        public string name { get; set; }
        public SizeArray[] sizes { get; set; }
        //public IFormFile image { get; set; }
    }

    public class SizeArray
    { 
        public string key { get; set; }
        public string estilo_talla_c_iid { get; set; }
        public Boolean check { get; set; }
    }

    public class Style2
    {
        public int estilo_c_iid { get; set; }
        public int estilo_talla_c_iid { get; set; }
        public string estilo_talla_c_vid { get; set; }
        public string estilo_c_vcodigo { get; set; }
        public string estilo_c_vnombre { get; set; }
        public string estilo_c_vdescripcion { get; set; }
        public int itm_c_iid { get; set; }
        public string marca_c_vid { get; set; }
        public string marca_categoria_c_vid { get; set; }
        public string marca_color_c_vid { get; set; }
        public string marcaColorDescription { get; set; }
        public string brandName { get; set; }
        public string categoryName { get; set; }
        public string colorName { get; set; }
        public List<SizeArray> sizeName { get; set; }
        public string itemName { get; set; }
    }

    public class NewColor
    {
        public string code { get; set; }
        public string brand { get; set; }
        public string color { get; set; }
        public string description { get; set; }
        public string color_m { get; set; }
        public Boolean own { get; set; }
        public string colorName { get; set; }
    }

    public class NewCategory
    {
        public string category { get; set; }
        public string brand { get; set; }
        public string material { get; set; }
        public string process { get; set; }
        public string category_m { get; set; }
    }
    
    public class NewCategoryDetail
    {
        public string category { get; set; }
        public string description { get; set; }
    }

    public class NewMarca
    {
        public string marca { get; set; }
        public string description { get; set; }
    }
}
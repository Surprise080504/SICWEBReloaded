using Microsoft.AspNetCore.Http;
using System;

namespace SICWEB.Models
{
    public class NewUser
    {
        public string name{ get; set; }
        public string lastname { get; set; }
        public string mlastname { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string networkuser { get; set; }
        public byte profile_id { get; set; }
        public bool estado { get; set; }
        public int method { get; set; }
        public IFormFile userImage { get; set; }
    }


}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SICWEB.DbFactory;
using SICWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SICWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public FileController(
            IHostingEnvironment _environment
        )
        {
            Environment = _environment;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Obsolete]
        public IActionResult GetImage([FromBody] IdKey item)
        {
            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(contentPath, "ClientApp/public/uploads");
            string filePath = Path.Combine(path, "estilo_image_" + item.id.ToString() + ".png");
            try
            {
                Byte[] b = System.IO.File.ReadAllBytes(filePath);
                return Ok("data:image/png;base64," + Convert.ToBase64String(b));
            }catch(Exception e)
            {
                return Ok(false);
            }
        }


    }
}

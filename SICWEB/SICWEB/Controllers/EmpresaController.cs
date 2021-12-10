using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SICWEB.DbFactory;
using SICWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SICWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmpresaController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly string _engine;

        public EmpresaController(
            MaintenanceMssqlDbContext context_MS,
            IConfiguration configuration
        )
        {
            _context_MS = context_MS;
            _engine = configuration.GetConnectionString("ActiveEngine");

        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allempresas()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.EMPRESA.ToArray());
                }
                catch (Exception e)
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetEmpresa([FromBody] IdKey pid)
        {
            if (_engine.Equals("MSSQL"))
            {
                var empresa = _context_MS.EMPRESA.Where(u => u.emp_c_iid.Equals(pid.id)).FirstOrDefault();
                if (empresa == null) return Conflict();
                return Ok(empresa);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allcentroscosto()
        {
            if (_engine.Equals("MSSQL"))
            {
                var centros_costo = _context_MS.EMP_CENTRO_COSTO.ToArray();
                if (centros_costo == null) return Conflict();
                return Ok(centros_costo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult alladdresses()
        {
            if (_engine.Equals("MSSQL"))
            {
                var addresses = _context_MS.EMP_DIRECCION.ToArray();
                if (addresses == null) return Conflict();
                return Ok(addresses);
            }
            else
            {
                return NotFound();
            }
        }
    }
}


    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ClientController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly string _engine;
        public ClientController(
            MaintenanceMssqlDbContext context_MS,
            IConfiguration configuration
        )
        {
            _context_MS = context_MS;
            _engine = configuration.GetConnectionString("ActiveEngine");

        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Clients([FromBody] SearchClientKey searchClientKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_CLIENTE>()
                            select new
                            {
                                ruc = A.cli_c_vdoc_id,
                                company = A.cli_c_vraz_soc,
                                ditem = A.cli_c_vrubro,
                                provider = A.cli_c_bproveedor,
                                client = A.cli_c_bcliente
                            };

                if (!(searchClientKey.ruc == null) && !searchClientKey.ruc.Equals(""))
                {
                    query = query.Where(a => a.ruc.Contains(searchClientKey.ruc));

                }
                if (!(searchClientKey.company == null) && !searchClientKey.company.Equals(""))
                {
                    query = query.Where(a => a.company.Contains(searchClientKey.company));
                }
                query = query.Where(a => a.provider.Equals(searchClientKey.provider));
                query = query.Where(a => a.client.Equals(searchClientKey.client));
                return Ok(query);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveClient([FromBody] NewClient client)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (client.id < 0)
                {
                    T_CLIENTE _c = new();
                    _c.cli_c_vdoc_id = client.ruc;
                    _c.cli_c_vraz_soc = client.company;
                    _c.cli_c_vpartida = client.snumber;
                    _c.cli_c_vrubro = client.ditem;
                    _c.cli_c_ctlf = client.phone;
                    if(client.anniversary != null) _c.cli_c_dfec_aniv = (DateTime)client.anniversary;
                    _c.cli_c_btipo_pers = client.person == 1;
                    _c.cli_c_bactivo = true;
                    _c.zona_rep_c_yid = client.delivery;
                    _c.cli_c_dfecharegistra = DateTime.Now;
                    _c.cli_c_dfechaactualiza = DateTime.Now;
                    if (client.constitution != null) _c.cli_c_dfec_const = (DateTime)client.constitution;
                    _c.cli_c_bproveedor = client.provider;
                    _c.cli_c_bcliente = client.client;
                    _context_MS.CLIENTE.Add(_c);
                    _context_MS.SaveChanges();
                    if (client.contact.Length > 0) {
                        for (int i = 0; i < client.contact.Length; i++)
                        {
                            NewContact contact = client.contact[i];
                            T_CLI_CONTACTO _item = new();
                            _item.cli_contac_c_cdoc_id = contact.identification;
                            _item.cli_contac_c_vnomb = contact.name;
                            _item.cli_contac_c_vape_pat = contact.surname;
                            _item.cli_contac_c_vape_mat = contact.lastname;
                            _item.cli_contac_c_ctlf = contact.landline;
                            _item.cli_contac_c_ccel = contact.phone;
                            _item.cli_contac_c_vcorreo = contact.email;
                            if (!contact.birthday.Equals(null)) _item.cli_contac_c_dfec_cump = (DateTime)contact.birthday;
                            _item.cli_contac_cargo_c_yid = (byte)contact.cargo;
                            _item.cli_contac_c_vobserv = contact.observations;
                            _item.cli_contac_c_bactivo = true;// (byte)contact.type;
                            _item.cli_c_vdoc_id = client.ruc;

                            _context_MS.CLI_CONTACTO.Add(_item);
                            _context_MS.SaveChanges();

                        }
                    }
                    if (client.direction.Length > 0)
                    {
                        for (int i = 0; i < client.direction.Length; i++)
                        {
                            NewDirection contact = client.direction[i];
                            T_CLI_DIRECCION _item = new();
                            _item.cli_c_vdoc_id = client.ruc;
                            _item.cli_direc_c_vdirec = contact.value;
                            _item.cli_direc_c_vdirec = contact.value;
                            _item.cli_direc_c_vdirec = contact.value;
                            _item.cli_direc_c_vdirec = contact.value;
                            _item.cli_direc_c_vdirec = contact.value;
                            _item.cli_direc_c_vdirec = contact.value;

                            _context_MS.CLI_DIRECCION.Add(_item);
                            _context_MS.SaveChanges();

                        }
                    }
                    return Ok();
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allNonas()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.ZONA_REPARTO.ToArray());
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
        public IActionResult allDepartment()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.DEPARTAMENTO.ToArray());
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

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allProvince([FromBody] IdKey2 did)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.PROVINCIA.Where(u => u.depa_c_ccod.Equals(did.id)).ToArray();
   
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allDistrict([FromBody] IdKey2 pid)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.DISTRITO.Where(u => u.prov_c_ccod.Equals(pid.id)).ToArray();

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allCargo()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.CLI_CONTAC_CARGO.ToArray());
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

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult FormatContact()
        {//[FromBody] NewContact contact
            if (_engine.Equals("MSSQL"))
            {
                if (true)
                {
                    var result = _context_MS.CLIENTE.Where(u => u.cli_c_vdoc_id.Equals("00000000000")).Count();

                    return Ok();
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return Ok();
            }
        }


        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveContact([FromBody] NewContact contact)
        {//[FromBody] NewContact contact
            if (_engine.Equals("MSSQL"))
            {
                if (true)
                {
                    T_CLI_CONTACTO _item = new();
                    _item.cli_contac_c_cdoc_id = contact.identification;
                    _item.cli_contac_c_vnomb = contact.name;
                    _item.cli_contac_c_vape_pat = contact.surname;
                    _item.cli_contac_c_vape_mat = contact.lastname;
                    _item.cli_contac_c_ctlf = contact.landline;
                    _item.cli_contac_c_ccel = contact.phone;
                    _item.cli_contac_c_vcorreo = contact.email;
                    if(!contact.birthday.Equals(null))_item.cli_contac_c_dfec_cump = (DateTime)contact.birthday;
                    _item.cli_contac_cargo_c_yid = (byte)contact.cargo;
                    _item.cli_contac_c_vobserv = contact.observations;
                    _item.cli_contac_c_bactivo = true;// (byte)contact.type;
                    _item.cli_c_vdoc_id = "10217898051";
                    
                    _context_MS.CLI_CONTACTO.Add(_item);
                    _context_MS.SaveChanges();
                    return Ok();
                }
                else 
                {
                    return Ok();
                }
            }
            else
            {
                return Ok();
            }
        }
    }
}

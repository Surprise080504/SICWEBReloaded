
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
    public class InsumosController : ControllerBase
    {
        private readonly ConfeccionMssqlDbContext _context_MS;
        private readonly MaintenanceMssqlDbContext _context_MS2;
        private readonly string _engine;
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public InsumosController(
            ConfeccionMssqlDbContext context_MS,
            MaintenanceMssqlDbContext context_MS2,
            IConfiguration configuration,
            IHostingEnvironment _environment
        )
        {
            _context_MS = context_MS;
            _context_MS2 = context_MS2;
            _engine = configuration.GetConnectionString("ActiveEngine");
            Environment = _environment;

        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allProcess()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.PROCESO.ToArray());
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
        public IActionResult getEstiloProcess([FromBody] IdKey2 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.ESTILO_PROCESO.Where(u => u.estilo_c_iid.Equals(int.Parse(id.id))).ToArray();
      
                var query = from A in _context_MS.Set<T_ESTILO_PROCESO>()
                            join B in _context_MS.Set<T_ESTILO_PROCESO_DETALLE>()
                                on A.esti_proceso_c_iid equals B.esti_proceso_c_iid
                            select new
                            {
                                A.estilo_c_iid,
                                A.esti_proceso_c_iid
                                ,
                                A.proceso_c_vid
                                ,
                                B.esti_proc_detalle_c_vdescripcion
                                ,
                                B.esti_proc_detalle_c_ecosto
                                ,
                                B.esti_proc_detalle_c_isegundos
                            };
                query = query.Where(c => c.estilo_c_iid.Equals(int.Parse(id.id)));
                return Ok(query);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEstiloInsumos([FromBody] IdKey5 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.ESTILO_INSUMO.Where(u => u.estilo_c_iid.Equals(int.Parse(id.id1))).Where(u => u.estilo_talla_c_iid.Equals(int.Parse(id.id2))).ToArray();


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
        public IActionResult saveEstiloInsumos([FromBody] NewInsumo insumos)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (insumos.id < 0)
                {
                    if (insumos.isReplicate > 0)
                    {
                        for (int i = 0; i < insumos.sizes.Length; i++)
                        {
                            if (insumos.sizes[i].check == false)
                            {
                                continue;
                            }

                            var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(insumos.estilo_c_iid)).Where(u => u.talla_c_vid.Equals(insumos.sizes[i].key)).FirstOrDefault();

                            var _insumosquery = _context_MS.ESTILO_INSUMO.Where(u => u.estilo_talla_c_iid.Equals(query.estilo_talla_c_iid)).Where(u => u.estilo_c_iid.Equals(insumos.estilo_c_iid)).ToList();

                            foreach (var item in _insumosquery)
                            {
                                _context_MS.ESTILO_INSUMO.Remove(item);
                            }
                            _context_MS.SaveChanges();

                            for (int ii = 0; ii < insumos.estiloInsumoses.Length; ii++)
                            {
                                EstiloInsumo _temp = insumos.estiloInsumoses[ii];

                                T_ESTILO_INSUMO _insumo = new();
                                _insumo.estilo_c_iid = insumos.estilo_c_iid;
                                _insumo.itm_c_iid = _temp.itm_c_iid;
                                _insumo.estilo_insumo_c_econsumo = _temp.estilo_insumo_c_econsumo;
                                _insumo.estilo_insumo_c_ecosto = _temp.estilo_insumo_c_ecosto;
                                _insumo.estilo_insumo_c_emerma = _temp.estilo_insumo_c_emerma;
                                _insumo.estilo_talla_c_iid = insumos.estilo_talla_c_iid;
                                _context_MS.ESTILO_INSUMO.Add(_insumo);
                                _context_MS.SaveChanges();
                            }
                        }
                        return Ok();
                    }
                    else
                    {
                        for (int i = 0; i < insumos.estiloInsumoses.Length; i++)
                        {
                            EstiloInsumo _temp = insumos.estiloInsumoses[i];
                            if (_temp.esti_insumo_c_iid > 0)
                            {
                                T_ESTILO_INSUMO _insumo = _context_MS.ESTILO_INSUMO.Where(u => u.esti_insumo_c_iid.Equals(_temp.esti_insumo_c_iid)).First();
                                _insumo.estilo_c_iid = insumos.estilo_c_iid;
                                _insumo.itm_c_iid = _temp.itm_c_iid;
                                _insumo.estilo_insumo_c_econsumo = _temp.estilo_insumo_c_econsumo;
                                _insumo.estilo_insumo_c_ecosto = _temp.estilo_insumo_c_ecosto;
                                _insumo.estilo_insumo_c_emerma = _temp.estilo_insumo_c_emerma;
                                _insumo.estilo_talla_c_iid = insumos.estilo_talla_c_iid;
                                _context_MS.ESTILO_INSUMO.Update(_insumo);
                                _context_MS.SaveChanges();
                            }
                            else
                            {
                                T_ESTILO_INSUMO _insumo = new();
                                _insumo.estilo_c_iid = insumos.estilo_c_iid;
                                _insumo.itm_c_iid = _temp.itm_c_iid;
                                _insumo.estilo_insumo_c_econsumo = _temp.estilo_insumo_c_econsumo;
                                _insumo.estilo_insumo_c_ecosto = _temp.estilo_insumo_c_ecosto;
                                _insumo.estilo_insumo_c_emerma = _temp.estilo_insumo_c_emerma;
                                _insumo.estilo_talla_c_iid = insumos.estilo_talla_c_iid;
                                _context_MS.ESTILO_INSUMO.Add(_insumo);
                                _context_MS.SaveChanges();
                            }


                        }

                        for (int i = 0; i < insumos.sizes.Length; i++)
                        {
                            if (insumos.sizes[i].check == false)
                            {
                                continue;
                            }
                            var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(insumos.estilo_c_iid)).Where(u => u.talla_c_vid.Equals(insumos.sizes[i].key)).FirstOrDefault();
                            if (insumos.estilo_talla_c_iid == query.estilo_talla_c_iid)
                            {
                                continue;
                            }
                            else
                            {

                                var _insumoquery = _context_MS.ESTILO_INSUMO.Where(u => u.estilo_talla_c_iid.Equals(query.estilo_talla_c_iid)).Where(u => u.estilo_c_iid.Equals(insumos.estilo_c_iid)).ToList();

                                foreach (var item in _insumoquery)
                                {
                                    _context_MS.ESTILO_INSUMO.Remove(item);
                                }
                                _context_MS.SaveChanges();
                                for (int ii = 0; ii < insumos.estiloInsumoses.Length; ii++)
                                {
                                    EstiloInsumo _temp = insumos.estiloInsumoses[ii];

                                    T_ESTILO_INSUMO _insumo = new();
                                    _insumo.estilo_c_iid = insumos.estilo_c_iid;
                                    _insumo.itm_c_iid = _temp.itm_c_iid;
                                    _insumo.estilo_insumo_c_econsumo = _temp.estilo_insumo_c_econsumo;
                                    _insumo.estilo_insumo_c_ecosto = _temp.estilo_insumo_c_ecosto;
                                    _insumo.estilo_insumo_c_emerma = _temp.estilo_insumo_c_emerma;
                                    _insumo.estilo_talla_c_iid = query.estilo_talla_c_iid;
                                    _context_MS.ESTILO_INSUMO.Add(_insumo);
                                    _context_MS.SaveChanges();
                                }
                            }
                        }

                        return Ok();
                    }

                }
                else
                {
                }
            }
            else
            {
                return Ok();
            }
            return Ok();
        }


        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEstiloData([FromBody] IdKey2 id)
        {

            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.ESTILO.Where(u => u.estilo_c_vcodigo.Contains(id.id)).FirstOrDefault();

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
        public IActionResult getItems()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = from A in _context_MS2.Set<T_ITEM>()
                                join B in _context_MS2.Set<T_UNIDAD_MEDIDA>()
                                on A.und_c_yid equals B.und_c_yid
                                select new
                                {
                                    id = A.itm_c_iid,
                                    code = A.itm_c_ccodigo,
                                    description = A.itm_c_vdescripcion,
                                    unit = B.und_c_vdesc
                                };

                    return Ok(query);
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
    }
}

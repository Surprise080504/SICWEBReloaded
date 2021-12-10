
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
    public class ProcessController : ControllerBase
    {
        private readonly ConfeccionMssqlDbContext _context_MS;
        private readonly MaintenanceMssqlDbContext _context_MS2;
        private readonly string _engine;
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public ProcessController(
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
        public IActionResult getCheckedTallas([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ESTILO_TALLA>()
                            where A.estilo_c_iid == id.id
                            select new
                            {
                                key = A.talla_c_vid,
                                check = true
                            };
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



        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEstiloProcess([FromBody] IdKey5 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.ESTILO_PROCESO.Where(u => u.estilo_c_iid.Equals(int.Parse(id.id1)))
                    .Where(u => u.estilo_talla_c_iid.Equals(int.Parse(id.id2))).ToArray();

                var parentQuery = from A in _context_MS.Set<T_ESTILO_PROCESO>()
                                  where A.estilo_c_iid == int.Parse(id.id1) && A.estilo_talla_c_iid == int.Parse(id.id2)
                                  select new EstiloProcessResult()
                                  {
                                      estilo_c_iid = A.estilo_c_iid,
                                      esti_proceso_c_iid = A.esti_proceso_c_iid,
                                      proceso_c_vid = A.proceso_c_vid,
                                      esti_proc_detalle_c_vdescripcion = "",
                                      esti_proc_detalle_c_ecosto = A.estilo_proceso_c_ecosto,
                                      esti_proc_detalle_c_isegundos = A.estilo_proceso_c_isegundos,
                                      esti_proc_detalle_c_iid = 0,
                                      estilo_talla_c_iid = A.estilo_talla_c_iid
                                  };

                var childQuery = from A in _context_MS.Set<T_ESTILO_PROCESO>()
                                 join B in _context_MS.Set<T_ESTILO_PROCESO_DETALLE>()
                                 on A.esti_proceso_c_iid equals B.esti_proceso_c_iid
                                 where A.estilo_c_iid == int.Parse(id.id1) && A.estilo_talla_c_iid == int.Parse(id.id2)
                                 orderby B.esti_proceso_c_yorden
                                 select new EstiloProcessResult()
                                 {
                                     estilo_c_iid = A.estilo_c_iid,
                                     esti_proceso_c_iid = A.esti_proceso_c_iid,
                                     proceso_c_vid = A.proceso_c_vid,
                                     esti_proc_detalle_c_vdescripcion = B.esti_proc_detalle_c_vdescripcion,
                                     esti_proc_detalle_c_ecosto = B.esti_proc_detalle_c_ecosto,
                                     esti_proc_detalle_c_isegundos = B.esti_proc_detalle_c_isegundos,
                                     esti_proc_detalle_c_iid = B.esti_proc_detalle_c_iid,
                                     estilo_talla_c_iid = A.estilo_talla_c_iid,
                                     esti_proceso_c_yorden = B.esti_proceso_c_yorden
                                 };

                //query = query.Where(c => c.estilo_c_iid.Equals(int.Parse(id.id1))).Where(c => c.estilo_talla_c_iid.Equals(int.Parse(id.id2)));

                var query = parentQuery.ToList();

                query.AddRange(childQuery.ToList());
                
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
        public IActionResult Saveprocess([FromBody] NewProcessItem item)
        {
            if (_engine.Equals("MSSQL"))
            {
                T_PROCESO _process = new();
                _process.proceso_c_vid = item.cid;
                _process.proceso_c_vdescripcion = item.vdesc;
                _context_MS.PROCESO.Add(_process);
                _context_MS.SaveChanges();

                return Ok();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult saveEstiloProcess([FromBody] NewProcess process)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (process.id < 0)
                {
                    if (process.isReplicate > 0)
                    {

                        for (int i = 0; i < process.sizes.Length; i++)
                        {
                            if (process.sizes[i].check == false)
                            {
                                continue;
                            }
                            var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(process.estilo_c_iid)).Where(u => u.talla_c_vid.Equals(process.sizes[i].key)).FirstOrDefault();
                            
                            var _processquery = _context_MS.ESTILO_PROCESO.Where(u => u.estilo_talla_c_iid.Equals(query.estilo_talla_c_iid)).Where(u => u.estilo_c_iid.Equals(process.estilo_c_iid)).ToList();

                            foreach (var item in _processquery)
                            {
                                var _processdetailquery = _context_MS.ESTILO_PROCESO_DETALLE.Where(u => u.esti_proceso_c_iid.Equals(item.esti_proceso_c_iid)).ToList();
                                foreach (var detailitem in _processdetailquery)
                                {
                                    _context_MS.ESTILO_PROCESO_DETALLE.Remove(detailitem);
                                }
                                _context_MS.SaveChanges();

                                _context_MS.ESTILO_PROCESO.Remove(item);
                            }
                            _context_MS.SaveChanges();

                            for (int ii = 0; ii < process.estiloProcesses.Length; ii++)
                            {
                                EstiloProcess _temp = process.estiloProcesses[ii];

                                T_ESTILO_PROCESO _process = new();
                                _process.estilo_c_iid = process.estilo_c_iid;
                                _process.proceso_c_vid = _temp.proceso_c_vid;
                                decimal esti_proc_detalle_c_ecosto = 0;
                                if (_temp.childinfo is not null)
                                {
                                    foreach (var _tempchild in _temp.childinfo)
                                    {
                                        esti_proc_detalle_c_ecosto += _tempchild.cost;
                                    }
                                }
                                if (esti_proc_detalle_c_ecosto == 0)
                                {
                                    esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                }
                                _process.estilo_proceso_c_ecosto = esti_proc_detalle_c_ecosto;


                                decimal esti_proc_detalle_c_isegundos = 0;
                                if (_temp.childinfo is not null)
                                {
                                    foreach (var _tempchild in _temp.childinfo)
                                    {
                                        esti_proc_detalle_c_isegundos += _tempchild.effot;
                                    }
                                }
                                if (esti_proc_detalle_c_isegundos == 0)
                                {
                                    esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                }
                                _process.estilo_proceso_c_isegundos = esti_proc_detalle_c_isegundos;

                                _process.estilo_talla_c_iid = process.estilo_talla_c_iid;
                                _context_MS.ESTILO_PROCESO.Add(_process);
                                _context_MS.SaveChanges();

                                if (_temp.childinfo is not null)
                                {
                                    int index = 0;
                                    foreach (var childinfo in _temp.childinfo)
                                    {
                                        T_ESTILO_PROCESO_DETALLE _childdetail = new();
                                        _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                        _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                        _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                        _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                        _childdetail.esti_proceso_c_yorden = (byte)index;
                                        _context_MS.ESTILO_PROCESO_DETALLE.Add(_childdetail);
                                        _context_MS.SaveChanges();

                                        index++;
                                    }
                                }
                            }
                        }
                        return Ok();
                    }
                    else
                    {

                        for (int i = 0; i < process.estiloProcesses.Length; i++)
                        {
                            EstiloProcess _temp = process.estiloProcesses[i];
                            if (_temp.esti_proceso_c_iid > 0)
                            {
                                T_ESTILO_PROCESO _process = _context_MS.ESTILO_PROCESO.Where(u => u.esti_proceso_c_iid.Equals(_temp.esti_proceso_c_iid)).First();
                                _process.estilo_c_iid = process.estilo_c_iid;
                                _process.proceso_c_vid = _temp.proceso_c_vid;
                                decimal esti_proc_detalle_c_ecosto = 0;
                                foreach (var _tempchild in _temp.childinfo)
                                {
                                    esti_proc_detalle_c_ecosto += _tempchild.cost;
                                }
                                if (esti_proc_detalle_c_ecosto == 0)
                                {
                                    esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                }
                                _process.estilo_proceso_c_ecosto = esti_proc_detalle_c_ecosto;

                                decimal esti_proc_detalle_c_isegundos = 0;
                                if (_temp.childinfo is not null)
                                {
                                    foreach (var _tempchild in _temp.childinfo)
                                    {
                                        esti_proc_detalle_c_isegundos += _tempchild.effot;
                                    }
                                }
                                if (esti_proc_detalle_c_isegundos == 0)
                                {
                                    esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                }
                                _process.estilo_proceso_c_isegundos = esti_proc_detalle_c_isegundos;

                                _process.estilo_talla_c_iid = process.estilo_talla_c_iid;
                                _context_MS.ESTILO_PROCESO.Update(_process);
                                _context_MS.SaveChanges();
                                //_detail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                //_detail.esti_proc_detalle_c_vdescripcion = _temp.esti_proc_detalle_c_vdescripcion;
                                //_detail.esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                //_detail.esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                //_detail.esti_proceso_c_yorden = 1;
                                //_context_MS.ESTILO_PROCESO_DETALLE.Update(_detail);
                                //_context_MS.SaveChanges();

                                if (_temp.childinfo is not null)
                                {
                                    int index = 0;
                                    foreach (var childinfo in _temp.childinfo)
                                    {
                                        if (childinfo.childId > 0)
                                        {
                                            T_ESTILO_PROCESO_DETALLE _childdetail = _context_MS.ESTILO_PROCESO_DETALLE.Where(u => u.esti_proc_detalle_c_iid.Equals(childinfo.childId)).First();
                                            _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                            _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                            _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                            _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                            _childdetail.esti_proceso_c_yorden = (byte)index;
                                            _context_MS.ESTILO_PROCESO_DETALLE.Update(_childdetail);
                                            _context_MS.SaveChanges();

                                            index++;
                                        }
                                        else
                                        {
                                            T_ESTILO_PROCESO_DETALLE _childdetail = new();
                                            _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                            _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                            _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                            _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                            _childdetail.esti_proceso_c_yorden = (byte)index;
                                            _context_MS.ESTILO_PROCESO_DETALLE.Add(_childdetail);
                                            _context_MS.SaveChanges();

                                            index++;
                                        }
                                    }

                                }
                            }
                            else
                            {
                                T_ESTILO_PROCESO _process = new();
                                _process.estilo_c_iid = process.estilo_c_iid;
                                _process.proceso_c_vid = _temp.proceso_c_vid;
                                decimal esti_proc_detalle_c_ecosto = 0;
                                if (_temp.childinfo is not null)
                                {
                                    foreach (var _tempchild in _temp.childinfo)
                                    {
                                        esti_proc_detalle_c_ecosto += _tempchild.cost;
                                    }
                                }
                                if (esti_proc_detalle_c_ecosto == 0)
                                {
                                    esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                }
                                _process.estilo_proceso_c_ecosto = esti_proc_detalle_c_ecosto;

                                decimal esti_proc_detalle_c_isegundos = 0;
                                if (_temp.childinfo is not null)
                                {
                                    foreach (var _tempchild in _temp.childinfo)
                                    {
                                        esti_proc_detalle_c_isegundos += _tempchild.effot;
                                    }
                                }
                                if (esti_proc_detalle_c_isegundos == 0)
                                {
                                    esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                }
                                _process.estilo_proceso_c_isegundos = esti_proc_detalle_c_isegundos;
                                _process.estilo_talla_c_iid = process.estilo_talla_c_iid;
                                _context_MS.ESTILO_PROCESO.Add(_process);
                                _context_MS.SaveChanges();
                                //_detail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                //_detail.esti_proc_detalle_c_vdescripcion = _temp.esti_proc_detalle_c_vdescripcion;
                                //_detail.esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                //_detail.esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                //_detail.esti_proceso_c_yorden = 1;
                                //_context_MS.ESTILO_PROCESO_DETALLE.Add(_detail);
                                //_context_MS.SaveChanges();

                                if (_temp.childinfo is not null)
                                {
                                    foreach (var childinfo in _temp.childinfo)
                                    {
                                        int index = 0;
                                        if (childinfo.childId > 0)
                                        {
                                            T_ESTILO_PROCESO_DETALLE _childdetail = _context_MS.ESTILO_PROCESO_DETALLE.Where(u => u.esti_proc_detalle_c_iid.Equals(childinfo.childId)).First();
                                            _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                            _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                            _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                            _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                            _childdetail.esti_proceso_c_yorden = (byte)index;
                                            _context_MS.ESTILO_PROCESO_DETALLE.Update(_childdetail);
                                            _context_MS.SaveChanges();

                                            index++;
                                        }
                                        else
                                        {
                                            T_ESTILO_PROCESO_DETALLE _childdetail = new();
                                            _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                            _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                            _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                            _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                            _childdetail.esti_proceso_c_yorden = (byte)index;
                                            _context_MS.ESTILO_PROCESO_DETALLE.Add(_childdetail);
                                            _context_MS.SaveChanges();

                                            index++;
                                        }
                                    }
                                }
                            }


                        }

                        for (int i = 0; i < process.sizes.Length; i++)
                        {
                            if (process.sizes[i].check == false)
                            {
                                continue;
                            }
                            var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(process.estilo_c_iid)).Where(u => u.talla_c_vid.Equals(process.sizes[i].key)).FirstOrDefault();
                            if (process.estilo_talla_c_iid == query.estilo_talla_c_iid)
                            {
                                continue;
                            }
                            else
                            {

                                var _processquery = _context_MS.ESTILO_PROCESO.Where(u => u.estilo_talla_c_iid.Equals(query.estilo_talla_c_iid)).Where(u => u.estilo_c_iid.Equals(process.estilo_c_iid)).ToList();

                                foreach (var item in _processquery)
                                {
                                    var _processdetailquery = _context_MS.ESTILO_PROCESO_DETALLE.Where(u => u.esti_proceso_c_iid.Equals(item.esti_proceso_c_iid)).ToList();
                                    foreach (var detailitem in _processdetailquery)
                                    {
                                        _context_MS.ESTILO_PROCESO_DETALLE.Remove(detailitem);
                                    }
                                    _context_MS.SaveChanges();

                                    _context_MS.ESTILO_PROCESO.Remove(item);
                                }
                                _context_MS.SaveChanges();
                                for (int ii = 0; ii < process.estiloProcesses.Length; ii++)
                                {
                                    EstiloProcess _temp = process.estiloProcesses[ii];

                                    T_ESTILO_PROCESO _process = new();
                                    _process.estilo_c_iid = process.estilo_c_iid;
                                    _process.proceso_c_vid = _temp.proceso_c_vid;
                                    decimal esti_proc_detalle_c_ecosto = 0;
                                    if (_temp.childinfo is not null)
                                    {
                                        foreach (var _tempchild in _temp.childinfo)
                                        {
                                            esti_proc_detalle_c_ecosto += _tempchild.cost;
                                        }
                                    }
                                    if (esti_proc_detalle_c_ecosto == 0)
                                    {
                                        esti_proc_detalle_c_ecosto = _temp.esti_proc_detalle_c_ecosto;
                                    }
                                    _process.estilo_proceso_c_ecosto = esti_proc_detalle_c_ecosto;

                                    decimal esti_proc_detalle_c_isegundos = 0;
                                    if (_temp.childinfo is not null)
                                    {
                                        foreach (var _tempchild in _temp.childinfo)
                                        {
                                            esti_proc_detalle_c_isegundos += _tempchild.effot;
                                        }
                                    }
                                    if (esti_proc_detalle_c_isegundos == 0)
                                    {
                                        esti_proc_detalle_c_isegundos = _temp.esti_proc_detalle_c_isegundos;
                                    }
                                    _process.estilo_proceso_c_isegundos = esti_proc_detalle_c_isegundos;
                                    _process.estilo_talla_c_iid = query.estilo_talla_c_iid;
                                    _context_MS.ESTILO_PROCESO.Add(_process);
                                    _context_MS.SaveChanges();

                                    if (_temp.childinfo is not null)
                                    {
                                        int index = 0;
                                        foreach (var childinfo in _temp.childinfo)
                                        {
                                            T_ESTILO_PROCESO_DETALLE _childdetail = new();
                                            _childdetail.esti_proceso_c_iid = _process.esti_proceso_c_iid;
                                            _childdetail.esti_proc_detalle_c_vdescripcion = childinfo.vdesc;
                                            _childdetail.esti_proc_detalle_c_isegundos = childinfo.effot;
                                            _childdetail.esti_proc_detalle_c_ecosto = childinfo.cost;
                                            _childdetail.esti_proceso_c_yorden = (byte) index;
                                            _context_MS.ESTILO_PROCESO_DETALLE.Add(_childdetail);
                                            _context_MS.SaveChanges();

                                            index++;
                                        }
                                    }
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

    }
}

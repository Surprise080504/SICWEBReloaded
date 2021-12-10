
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
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
    public class ItemController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly ConfeccionMssqlDbContext _context_SS;
        private readonly MainMssqlDbContext _context_US;
        private readonly string _engine;
        [Obsolete]
        private IHostingEnvironment Environment;
        public ItemController(
            MaintenanceMssqlDbContext context_MS,
            ConfeccionMssqlDbContext context_SS,
            MainMssqlDbContext context_US,
            IConfiguration configuration,
            IHostingEnvironment _environment
        )
        {
            _context_MS = context_MS;
            _context_SS = context_SS;
            _context_US = context_US;
            _engine = configuration.GetConnectionString("ActiveEngine");
            Environment = _environment;

        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Allfamilies()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = from A in _context_MS.ITEM_FAMILIA
                                join B in _context_MS.SEGMENTO
                                    on A.segmento_c_yid equals B.Segmento_c_yid
                                where B.Segmento_c_bactivo select A;

                    return Ok(query.ToArray());

                    //return Ok(_context_MS.ITEM_FAMILIA.ToArray());
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
        public IActionResult Families([FromBody] IdKey key)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.ITEM_FAMILIA.Where(c => c.segmento_c_yid.Equals(Convert.ToByte(key.id))).ToArray());
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
        public IActionResult subfamilies([FromBody] IdKey key)
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.ITEM_SUB_FAMILIA.Where(c => c.isf_c_ifm_iid.Equals(key.id)).ToList());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult units()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.UNIDAD_MEDIDA.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult segments()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.SEGMENTO.Where(x => x.Segmento_c_bactivo).ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult products()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.PRODUCTO_PARTIDA.ToArray().Take(10));
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult product([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.PRODUCTO_PARTIDA.Where(u => u.isf_c_iid.Equals(id.id)).ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult familysub([FromBody] IdKey pid)
        {
            if (_engine.Equals("MSSQL"))
            {
                //var product = _context_MS.PRODUCTO_PARTIDA.Where(u => u.pro_partida_c_iid.Equals(pid.id)).FirstOrDefault();
                //if (product == null) return Conflict();
                //var subFamily = _context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_iid.Equals(product.isf_c_iid)).FirstOrDefault();
                //if (subFamily == null) return Conflict();
                //var family = _context_MS.ITEM_FAMILIA.Where(u => u.ifm_c_iid.Equals(subFamily.isf_c_ifm_iid)).FirstOrDefault();
                //if (family == null) return Conflict();
                var subFamily = _context_MS.ITEM_SUB_FAMILIA.ToList();
                if (subFamily == null) return Conflict();
                var family = from A in _context_MS.ITEM_FAMILIA
                            join B in _context_MS.SEGMENTO
                                on A.segmento_c_yid equals B.Segmento_c_yid
                            where B.Segmento_c_bactivo
                            select A;

                if (family == null) return Conflict();
                FamilySubList familySub = new();

                familySub.Family = family.ToList();
                familySub.SubFamily = subFamily;
                return Ok(familySub);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getItems([FromBody] SearchKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_MS.Set<T_ITEM>()
                            join E in _context_MS.Set<T_UNIDAD_MEDIDA>()
                                on A.und_c_yid equals E.und_c_yid
                            join B in _context_MS.Set<T_PRODUCTO_PARTIDA>()
                                on A.pro_partida_c_iid equals B.pro_partida_c_iid
                            join C in _context_MS.Set<T_ITEM_SUB_FAMILIA>()
                                on B.isf_c_iid equals C.isf_c_iid
                            join D in _context_MS.Set<T_ITEM_FAMILIA>()
                                on C.isf_c_ifm_iid equals D.ifm_c_iid
                            select new
                            {
                                A.itm_c_iid,
                                A.itm_c_vdescripcion
                                ,
                                A.itm_c_dprecio_compra
                                ,
                                A.itm_c_dprecio_venta
                                ,
                                A.itm_c_ccodigo
                                ,
                                A.pro_partida_c_iid
                                ,
                                C.isf_c_vdesc,
                                C.isf_c_iid,
                                D.ifm_c_des,
                                D.ifm_c_iid,
                                E.und_c_yid,
                                E.und_c_vdesc,
                                checkstate = false,
                                quantity = 1
                            };

                if (!(searchKey.code == null) && !searchKey.code.Equals(""))
                {
                    query = query.Where(c => c.itm_c_ccodigo.Contains(searchKey.code));

                }
                if (!(searchKey.description == null) && !searchKey.description.Equals(""))
                {
                    query = query.Where(c => c.itm_c_vdescripcion.Contains(searchKey.description));
                }
                if (searchKey.subFamily > -1)
                {
                    query = query.Where(c => c.isf_c_iid.Equals(searchKey.subFamily));
                }
                if (searchKey.family > -1)
                {
                    query = query.Where(c => c.ifm_c_iid.Equals(searchKey.family));
                }

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
        public IActionResult getProveedorChecked([FromBody] SearchProveedorKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_MS.Set<T_CLIENTE>()
                            where A.cli_c_bproveedor == true
                            select new
                            {
                                ruc = A.cli_c_vdoc_id,
                                social = A.cli_c_vraz_soc
                            };

                if (!(searchKey.ruc == null) && !searchKey.ruc.Equals(""))
                {
                    query = query.Where(c => c.ruc.Contains(searchKey.ruc));

                }
                if (!(searchKey.social == null) && !searchKey.social.Equals(""))
                {
                    query = query.Where(c => c.social.Contains(searchKey.social));
                }

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
        public IActionResult Items([FromBody] SearchKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_MS.Set<T_ITEM>()
                            join E in _context_MS.Set<T_UNIDAD_MEDIDA>()
                                on A.und_c_yid equals E.und_c_yid
                            join B in _context_MS.Set<T_PRODUCTO_PARTIDA>()
                                on A.pro_partida_c_iid equals B.pro_partida_c_iid
                            join C in _context_MS.Set<T_ITEM_SUB_FAMILIA>()
                                on B.isf_c_iid equals C.isf_c_iid
                            join D in _context_MS.Set<T_ITEM_FAMILIA>()
                                on C.isf_c_ifm_iid equals D.ifm_c_iid
                            select new
                            {
                                A.itm_c_iid,
                                A.itm_c_vdescripcion
                                ,
                                A.itm_c_dprecio_compra
                                ,
                                A.itm_c_dprecio_venta
                                ,
                                A.itm_c_ccodigo
                                ,
                                A.pro_partida_c_iid
                                ,
                                C.isf_c_vdesc,
                                C.isf_c_iid,
                                D.ifm_c_des,
                                D.ifm_c_iid,
                                E.und_c_yid,
                                E.und_c_vdesc,
                            };

                if (!(searchKey.code == null) && !searchKey.code.Equals(""))
                {
                    query = query.Where(c => c.itm_c_ccodigo.Contains(searchKey.code));

                }
                if (!(searchKey.description == null) && !searchKey.description.Equals(""))
                {
                    query = query.Where(c => c.itm_c_vdescripcion.Contains(searchKey.description));
                }
                if (searchKey.subFamily > -1)
                {
                    query = query.Where(c => c.isf_c_iid.Equals(searchKey.subFamily));
                }
                if (searchKey.family > -1)
                {
                    query = query.Where(c => c.ifm_c_iid.Equals(searchKey.family));
                }

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
        public IActionResult SaveUnit([FromBody] NewUnit newUnit)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.UNIDAD_MEDIDA.Where(u => u.und_c_vdesc.Equals(newUnit.unit)).Any())
                    return Conflict();
                //return Task.FromResult(Ok(_context_MS.UNIDAD_MEDIDA.ToArray()));
                byte max = _context_MS.UNIDAD_MEDIDA.Max(i => i.und_c_yid);
                max++;
                T_UNIDAD_MEDIDA _unit = new();
                _unit.und_c_yid = max;
                _unit.und_c_vdesc = newUnit.unit;
                _unit.und_c_bactivo = newUnit.flag;
                _context_MS.UNIDAD_MEDIDA.Add(_unit);
                _context_MS.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Checkimagefile([FromBody] IdKey2 item)
        {
            if (item.id == null)
            {
                return Ok(false);
            }
            string contentPath = this.Environment.ContentRootPath;
            var uploadDirectory = "ClientApp/public/uploads/";
            var uploadPath = Path.Combine(contentPath, uploadDirectory);
            var filePath = Path.Combine(uploadPath, item.id);

            var exists = System.IO.File.Exists(filePath);

            return Ok(exists);
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveFamily([FromBody] NewFamily newFamily)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.ITEM_FAMILIA.Where(u => u.ifm_c_des.Equals(newFamily.family) && u.segmento_c_yid.Equals(Convert.ToByte(newFamily.segId))).Any())
                    return Conflict();
                T_ITEM_FAMILIA _family = new();
                _family.ifm_c_des = newFamily.family;
                _family.ifm_c_bactivo = newFamily.flag;
                _family.segmento_c_yid = (byte)newFamily.segId;
                _context_MS.ITEM_FAMILIA.Add(_family);
                _context_MS.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveSubFamily([FromBody] NewSubFamily newSubFamily)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_vdesc.Equals(newSubFamily.subfamily) && u.isf_c_ifm_iid.Equals(newSubFamily.fid)).Any())
                    return Conflict();
                T_ITEM_SUB_FAMILIA _subFmily = new();
                _subFmily.isf_c_ifm_iid = newSubFamily.fid;
                _subFmily.isf_c_vdesc = newSubFamily.subfamily;
                _subFmily.isf_c_bactivo = newSubFamily.flag;
                _context_MS.ITEM_SUB_FAMILIA.Add(_subFmily);
                _context_MS.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Saveitem([FromBody] NewItem item)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (item.id < 0)
                {
                    T_ITEM _item = new();
                    _item.itm_c_ccodigo = item.code;
                    _item.itm_c_vdescripcion = item.description;

                    _item.itm_c_dprecio_compra = item.purchaseprice;
                    _item.itm_c_dprecio_venta = item.saleprice;
                    _item.und_c_yid = item.unit;
                    _item.pro_partida_c_iid = item.pid;
                    _item.itm_c_bactivo = true;
                    //_item.Item_c_zregistro = DateTime.Now;
                    //_item.Item_c_vreg_usuario = User.Identity.Name;
                    _context_MS.ITEM.Add(_item);
                    _context_MS.SaveChanges();
                    return Ok(_item.itm_c_iid);
                }
                else
                {
                    var _item = _context_MS.ITEM.Where(e => e.itm_c_iid.Equals(item.id)).FirstOrDefault();
                    _item.itm_c_ccodigo = item.code;
                    _item.itm_c_vdescripcion = item.description;

                    _item.itm_c_dprecio_compra = item.purchaseprice;
                    _item.itm_c_dprecio_venta = item.saleprice;
                    _item.und_c_yid = item.unit;
                    _item.pro_partida_c_iid = item.pid;
                    _item.itm_c_bactivo = true;
                    //_item.Item_c_zmodificacion = DateTime.Now;
                    //_item.Item_c_vmod_usuario = User.Identity.Name;
                    _context_MS.ITEM.Update(_item);
                    _context_MS.SaveChanges();
                    return Ok(_item.itm_c_iid);
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
        [Obsolete]
        public async Task<IActionResult> imageUploadAsync([FromForm] IFormFile image, [FromForm] String _id)
        {
            if (_engine.Equals("MSSQL"))
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                var uploadDirectory = "ClientApp/public/uploads/";
                var uploadPath = Path.Combine(contentPath, uploadDirectory);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string fileName = Path.GetFileName(image.FileName);

                using (var stream = new FileStream(Path.Combine(uploadPath, "estilo_item_image_" + _id + ".png"), FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getPermissionOption([FromBody] IdKey2 item)
        {

            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_US.Set<T_PERFIL_MENU_OPCION>()
                            join B in _context_US.Set<T_MENU_OPCION>() on A.Menu_opcion_c_iid equals B.Menu_opcion_c_iid
                            join C in _context_US.Set<T_OPCION>() on B.Opc_c_iid equals C.Opc_c_iid
                            join D in _context_US.Set<T_USUARIO_PERFIL>() on A.Perf_c_yid equals D.Perf_c_yid
                            where D.Usua_c_cdoc_id == item.id
                            select new
                            {
                                option = C.Opc_c_vdesc,
                                enable = 1,
                            };

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
        public IActionResult Deleteitem([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                //if (_context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_vdesc.Equals(newSubFamily.subfamily) && u.isf_c_ifm_iid.Equals(newSubFamily.family)).Any())
                //    return Ok();
                //return Task.FromResult(Ok(_context_MS.UNIDAD_MEDIDA.ToArray()));


                //return Ok("2");
                var _isUsed = _context_SS.ESTILO.Where(u => u.itm_c_iid.Equals(item.id)).Any();
                if (_isUsed)
                {
                    return Ok(false);
                }

                var _isUsedInsumos = _context_SS.ESTILO_INSUMO.Where(u => u.itm_c_iid.Equals(item.id)).Any();
                if (_isUsedInsumos)
                {
                    return Ok(false);
                }

                var _item = _context_MS.ITEM.Where(u => u.itm_c_iid.Equals(item.id)).FirstOrDefault();
                if (!(_item == null))
                {
                    _context_MS.ITEM.Remove(_item);
                    _context_MS.SaveChanges();
                    return Ok(true);
                }
                else
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
        public async Task<IActionResult> Download()
        {
            if (_engine.Equals("MSSQL"))
            {
                await Task.Yield();
                //if (_context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_vdesc.Equals(newSubFamily.subfamily) && u.isf_c_ifm_iid.Equals(newSubFamily.family)).Any())
                //    return Ok();
                //return Task.FromResult(Ok(_context_MS.UNIDAD_MEDIDA.ToArray()));

                var query = from A in _context_MS.Set<T_ITEM>()
                            join E in _context_MS.Set<T_UNIDAD_MEDIDA>()
                                on A.und_c_yid equals E.und_c_yid
                            join B in _context_MS.Set<T_PRODUCTO_PARTIDA>()
                                on A.pro_partida_c_iid equals B.pro_partida_c_iid
                            join C in _context_MS.Set<T_ITEM_SUB_FAMILIA>()
                                on B.isf_c_iid equals C.isf_c_iid
                            join D in _context_MS.Set<T_ITEM_FAMILIA>()
                                on C.isf_c_ifm_iid equals D.ifm_c_iid
                            select new
                            {
                                A.itm_c_iid,
                                A.itm_c_vdescripcion
                                ,
                                A.itm_c_dprecio_compra
                                ,
                                A.itm_c_dprecio_venta
                                ,
                                A.itm_c_ccodigo
                                ,
                                A.pro_partida_c_iid
                                ,
                                C.isf_c_vdesc,
                                C.isf_c_iid,
                                D.ifm_c_des,
                                D.ifm_c_iid,
                                E.und_c_yid,
                                E.und_c_vdesc,
                            };
                

                var stream = new MemoryStream();
                using (ExcelPackage excelPackage = new ExcelPackage(stream))
                {
                    var workSheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Cells.LoadFromCollection(query.ToList(), true);
                    excelPackage.Save();

                }
                stream.Position = 0;
                string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                //return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);


                return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                //    //return Ok("2");
                //    var _item = _context_MS.ITEM.Where(u => u.itm_c_iid.Equals(item.id)).FirstOrDefault();
                //if (!(_item == null))
                //{
                //    _context_MS.ITEM.Remove(_item);
                //    _context_MS.SaveChanges();
                //    return Ok();
                //}
                //else
                //{
                //    return NotFound();
                //}
            }
            else
            {
                return NotFound();
            }
        }


    }
}

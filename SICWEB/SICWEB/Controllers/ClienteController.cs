
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
    public class ClienteController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly string _engine;
        public ClienteController(
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
        public IActionResult clientes([FromBody] SearchOrdenKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.PRODUCTO_PARTIDA.ToArray());
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
                var product = _context_MS.PRODUCTO_PARTIDA.Where(u => u.pro_partida_c_iid.Equals(pid.id)).FirstOrDefault();
                if(product == null) return Conflict();
                var subFamily = _context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_iid.Equals(product.isf_c_iid)).FirstOrDefault();
                if (subFamily == null) return Conflict();
                var family = _context_MS.ITEM_FAMILIA.Where(u => u.ifm_c_iid.Equals(subFamily.isf_c_ifm_iid)).FirstOrDefault();
                if (family == null) return Conflict();
                FamilySub familySub = new();

                familySub.Family = family;
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
                                E.und_c_yid
                            };

                if (!(searchKey.code == null) && !searchKey.code.Equals("")) {
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
                if (item.id<0)
                {
                    T_ITEM _item = new();
                    _item.itm_c_ccodigo = item.code;
                    _item.itm_c_vdescripcion = item.description;

                    _item.itm_c_dprecio_compra = item.purchaseprice;
                    _item.itm_c_dprecio_venta = item.saleprice;
                    _item.und_c_yid = item.unit;
                    _item.pro_partida_c_iid = item.pid;
                    _item.itm_c_bactivo = true;
                    _context_MS.ITEM.Add(_item);
                    _context_MS.SaveChanges();
                    return Ok();
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
                    _context_MS.ITEM.Update(_item);
                    _context_MS.SaveChanges();
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
        public IActionResult Deleteitem([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                //if (_context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_vdesc.Equals(newSubFamily.subfamily) && u.isf_c_ifm_iid.Equals(newSubFamily.family)).Any())
                //    return Ok();
                //return Task.FromResult(Ok(_context_MS.UNIDAD_MEDIDA.ToArray()));
                var _item = _context_MS.ITEM.Where(u => u.itm_c_iid.Equals(item.id)).FirstOrDefault();
                if (!(_item == null))
                {
                    _context_MS.ITEM.Remove(_item);
                    _context_MS.SaveChanges();
                    return Ok();
                }
                else {
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

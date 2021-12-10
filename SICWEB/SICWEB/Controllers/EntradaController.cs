
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
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
    public class EntradaController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly string _engine;
        public EntradaController(
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
        public IActionResult getEstados()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.Mov_estado.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEntradas([FromBody] SearchEntradaKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_MOVIMIENTO_ENTRADA>()
                            join B in _context_MS.Set<T_MOV_ESTADO>() on A.mve_c_iestado equals B.mov_estado_iid into C
                            from D in C.DefaultIfEmpty()
                            join E in
                             (
                                 from AA in _context_MS.Set<T_ORDEN_DE_COMPRA>()
                                 select new
                                 {
                                     AA.odc_c_iid,
                                     AA.prov_c_vdoc_id,
                                     AA.odc_c_cserie,
                                     odc_c_vcodigo = AA.odc_c_iid.ToString().PadLeft(9, '0')
                                 }
                             ) on A.odc_c_iid equals E.odc_c_iid into F
                            from G in F.DefaultIfEmpty()
                            join H in
                            (
                                from AA in _context_MS.Set<T_CLIENTE>()
                                select new
                                {
                                    AA.cli_c_vdoc_id,
                                    AA.cli_c_vraz_soc
                                }
                            ) on G.prov_c_vdoc_id equals H.cli_c_vdoc_id
                            where A.mve_c_bactivo == true
                            select new
                            {
                                id = A.mve_c_iid,
                                ruc = G.prov_c_vdoc_id,
                                G.odc_c_cserie,
                                G.odc_c_vcodigo,
                                razonsocial = H.cli_c_vraz_soc,
                                fecha = A.mve_c_zfecharegistro,
                                estado = D.mov_estado_vdescrpcion,
                                estado_id = D.mov_estado_iid,
                                registdate = A.mve_c_zfecharegistro
                            };

                if (searchKey.ruc != "")
                {
                    query = query.Where(u => u.ruc.Contains(searchKey.ruc));
                }

                if (searchKey.razonsocial != "")
                {
                    query = query.Where(u => u.razonsocial.Contains(searchKey.razonsocial));
                }

                if (searchKey.estado != -1)
                {
                    query = query.Where(u => u.estado_id.Equals(searchKey.estado));
                }

                if (searchKey.desde != "")
                {
                    query = query.Where(u => u.registdate.Date >= DateTime.Parse(searchKey.desde));
                }

                if (searchKey.hasta != "")
                {
                    query = query.Where(u => u.registdate.Date <= DateTime.Parse(searchKey.hasta));
                }

                var result = query.ToList();

                return Ok(result);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getOrdens([FromBody] SearchOrdenKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA>()
                            join B in _context_MS.Set<T_ODC_ESTADO>() on A.odc_c_iestado equals B.odc_estado_iid //into C
                            //from D in C.DefaultIfEmpty()
                            join E in
                             (
                                 from AA in _context_MS.Set<T_PARAMETRO_DET>()
                                 where AA.par_c_iid == 5
                                 select new
                                 {
                                     AA.par_det_c_iid,
                                     AA.par_det_c_vdesc
                                 }
                             ) on A.odc_c_ymoneda equals E.par_det_c_iid  //into F
                            //from G in F.DefaultIfEmpty()
                            join H in
                            (
                                from AA in _context_MS.Set<T_CLIENTE>()
                                where AA.cli_c_bproveedor == true
                                select new
                                {
                                    AA.cli_c_vdoc_id,
                                    AA.cli_c_vraz_soc
                                }
                            ) on A.prov_c_vdoc_id equals H.cli_c_vdoc_id
                            select new
                            {
                                id = A.odc_c_iid,
                                serie = A.odc_c_cserie,
                                codigo = A.odc_c_iid.ToString().PadLeft(9, '0'),
                                ruc = A.prov_c_vdoc_id,
                                prov = H.cli_c_vraz_soc,
                                estado = B.odc_estado_vdescripcion,
                                moneda = E.par_det_c_vdesc,// == null ? "" : G.par_det_c_vdesc,
                                monedaid = E.par_det_c_iid, //== null ? 0 : G.par_det_c_iid,
                                monototal = string.Format("{0:0,0.00}", A.odc_c_etotal),
                                odc_c_iestado = A.odc_c_iestado,
                                fechaemi = A.odc_c_zfechaemi,
                                fechaRegistro = A.odc_c_zfecharegistro,
                                fechaEntregaIni = A.odc_c_zfechaentrega_ini,
                                fechaEntregaFin = A.odc_c_zfechaentrega_fin
                            };

                if (searchKey.ruc != "")
                {
                    query = query.Where(u => u.ruc.Contains(searchKey.ruc));
                }

                if (searchKey.estado != -1)
                {
                    query = query.Where(u => u.odc_c_iestado.Equals(searchKey.estado));
                }

                if (searchKey.moneda != -1)
                {
                    query = query.Where(u => u.monedaid.Equals(searchKey.moneda));
                }

                query = query.Where(x => x.estado == "EN PROCESO");

                var result = query.ToList();

                return Ok(result);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getItems([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA_DET>()
                            join B in _context_MS.Set<T_ITEM>() on A.odc_c_iitemid equals B.itm_c_iid into C
                            from D in C.DefaultIfEmpty()
                            join E in (
                                from F in _context_MS.Set<T_MOVIMIENTO_ENTRADA_DETALLE>()
                                join FF in _context_MS.Set<T_MOVIMIENTO_ENTRADA>() on F.mve_c_iid equals FF.mve_c_iid into GG
                                from LL in GG.DefaultIfEmpty()
                                where LL.mve_c_iestado == 3
                                group F by F.mve_c_iocdet_id into G
                                select new
                                {
                                    id = G.Key,
                                    sum = G.Sum(x => x.mve_c_ecant_recibida)
                                }
                            ) on A.odc_det_c_iid equals E.id into H
                            from I in H.DefaultIfEmpty()
                            where A.odc_c_iid == id.id
                            select new
                            {
                                item_c_iid = D.itm_c_iid,
                                iocdet_id = A.odc_det_c_iid,
                                pedida = A.odc_c_ecantidad,
                                descripcion = D.itm_c_vdescripcion,
                                recibida = 0,
                                atendida = I.sum == null ? 0 : I.sum,
                                maxatendida = Math.Round(A.odc_c_ecantidad / 5) + A.odc_c_ecantidad
                            };

                var result = query.ToList();
                return Ok(result);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getAlmacens([FromBody] SearchAlmacenKey serachKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ALMACEN>()
                            select new
                            {
                                id = A.alm_c_iid,
                                descripcion = A.alm_c_vdesc
                            };
                if (serachKey.descripcion != "")
                {
                    query = query.Where(u => u.descripcion.Contains(serachKey.descripcion));
                }
                var result = query.ToList();

                return Ok(result);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult saveEntrada([FromBody] NewEntrada entrada)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (entrada.mve_c_iid < 0)
                {
                    try
                    {
                        T_MOVIMIENTO_ENTRADA _entrada = new();
                        _entrada.odc_c_iid = entrada.odc_c_iid;
                        _entrada.mve_c_zfecharegistro = DateTime.Now;
                        _entrada.mve_c_zguiafecha = entrada.mve_c_zguiafecha;
                        _entrada.mve_c_zfacturafecha = entrada.mve_c_zfacturafecha;
                        _entrada.mve_c_vguiacodigo = entrada.mve_c_vguiacodigo;
                        _entrada.mve_c_vfacturacodigo = entrada.mve_c_vfacturacodigo;
                        _entrada.mve_c_iidalmacen = entrada.mve_c_iidalmacen;
                        _entrada.mve_c_bactivo = true;
                        _entrada.mve_c_iestado = entrada.mve_c_iestado;
                        _entrada.mve_c_vdesestado = entrada.mve_c_vdesestado;
                        _entrada.mve_c_vobservacion = entrada.mve_c_vobservacion;
                        _entrada.mve_c_bingresado = false;

                        _context_MS.Movimiento_entrada.Add(_entrada);
                        _context_MS.SaveChanges();

                        var count = 0;

                        foreach (var item in entrada.items)
                        {
                            if ((item.recibida + item.atendida) >= item.pedida) count++;
                            T_MOVIMIENTO_ENTRADA_DETALLE _entrada_det = new();
                            _entrada_det.mve_c_iid = _entrada.mve_c_iid;
                            _entrada_det.mve_c_ecant_pedida = item.pedida;
                            _entrada_det.mve_c_ecant_recibida = item.recibida;
                            _entrada_det.mve_c_vdescripcion_item = item.descripcion;
                            _entrada_det.mve_c_iocdet_id = item.iocdet_id;
                            _entrada_det.item_c_iid = item.item_c_iid;

                            _context_MS.Movimiento_entrada_detalle.Add(_entrada_det);
                        }
                        _context_MS.SaveChanges();

                        if (count == entrada.items.Length)
                        {
                            var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(_entrada.mve_c_iid)).FirstOrDefault();
                            movimientoItem.mve_c_iestado = 3;
                            movimientoItem.mve_c_vdesestado = "CERRADO";
                            movimientoItem.mve_c_bingresado = true;
                            _context_MS.Movimiento_entrada.Update(movimientoItem);
                            _context_MS.SaveChanges();

                            var odcItem = _context_MS.Orden_de_compra.Where(e => e.odc_c_iid.Equals(entrada.odc_c_iid)).FirstOrDefault();
                            odcItem.odc_c_iestado = 3;
                            odcItem.odc_c_vdescestado = "CERRADA";

                            _context_MS.Orden_de_compra.Update(odcItem);
                            _context_MS.SaveChanges();

                            foreach (var item in entrada.items)
                            {
                                T_ITEM_ALMACEN _item_almacen = new();
                                _item_almacen.alm_c_iid = entrada.mve_c_iidalmacen;
                                _item_almacen.itm_c_iid = item.item_c_iid;
                                _item_almacen.itm_alm_c_ecantidad = item.pedida;
                                _context_MS.Item_almacen.Add(_item_almacen);
                            }
                            _context_MS.SaveChanges();
                        }

                        return Ok(_entrada.mve_c_iid);
                    }
                    catch (Exception e)
                    {
                        return Ok(-1);
                    }
                }
                else
                {
                    try
                    {
                        var _item = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(entrada.mve_c_iid)).FirstOrDefault();
                        _item.odc_c_iid = entrada.odc_c_iid;
                        _item.mve_c_zguiafecha = entrada.mve_c_zguiafecha;
                        _item.mve_c_zfacturafecha = entrada.mve_c_zfacturafecha;
                        _item.mve_c_vguiacodigo = entrada.mve_c_vguiacodigo;
                        _item.mve_c_vfacturacodigo = entrada.mve_c_vfacturacodigo;
                        _item.mve_c_iidalmacen = entrada.mve_c_iidalmacen;
                        _item.mve_c_bactivo = true;
                        _item.mve_c_iestado = entrada.mve_c_iestado;
                        _item.mve_c_vdesestado = entrada.mve_c_vdesestado;
                        _item.mve_c_vobservacion = entrada.mve_c_vobservacion;
                        _item.mve_c_bingresado = false;

                        _context_MS.Movimiento_entrada.Update(_item);
                        _context_MS.SaveChanges();

                        var detailItems = _context_MS.Movimiento_entrada_detalle.Where(e => e.mve_c_iid.Equals(entrada.mve_c_iid));

                        foreach (var _detailitem in detailItems)
                        {
                            _context_MS.Movimiento_entrada_detalle.Remove(_detailitem);
                        }
                        _context_MS.SaveChanges();

                        var count1 = 0;

                        foreach (var entradaItem in entrada.items)
                        {
                            if ((entradaItem.recibida + entradaItem.atendida) >= entradaItem.pedida) count1++;
                            T_MOVIMIENTO_ENTRADA_DETALLE _entrada_det = new();
                            _entrada_det.mve_c_iid = entrada.mve_c_iid;
                            _entrada_det.mve_c_ecant_pedida = entradaItem.pedida;
                            _entrada_det.mve_c_ecant_recibida = entradaItem.recibida;
                            _entrada_det.mve_c_vdescripcion_item = entradaItem.descripcion;
                            _entrada_det.mve_c_iocdet_id = entradaItem.iocdet_id;
                            _entrada_det.item_c_iid = entradaItem.item_c_iid;

                            _context_MS.Movimiento_entrada_detalle.Add(_entrada_det);
                        }
                        _context_MS.SaveChanges();

                        if (count1 == entrada.items.Length)
                        {
                            var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(entrada.mve_c_iid)).FirstOrDefault();
                            movimientoItem.mve_c_iestado = 3;
                            movimientoItem.mve_c_vdesestado = "CERRADO";
                            movimientoItem.mve_c_bingresado = true;
                            _context_MS.Movimiento_entrada.Update(movimientoItem);
                            _context_MS.SaveChanges();

                            var odcItem = _context_MS.Orden_de_compra.Where(e => e.odc_c_iid.Equals(entrada.odc_c_iid)).FirstOrDefault();
                            odcItem.odc_c_iestado = 3;
                            odcItem.odc_c_vdescestado = "CERRADA";

                            _context_MS.Orden_de_compra.Update(odcItem);
                            _context_MS.SaveChanges();

                            foreach (var item in entrada.items)
                            {
                                if (item.recibida > 0)
                                {
                                    T_ITEM_ALMACEN _item_almacen = new();
                                    _item_almacen.alm_c_iid = entrada.mve_c_iidalmacen;
                                    _item_almacen.itm_c_iid = item.item_c_iid;
                                    _item_almacen.itm_alm_c_ecantidad = item.recibida;
                                    _context_MS.Item_almacen.Add(_item_almacen);
                                }
                            }
                            _context_MS.SaveChanges();
                        }

                        return Ok(_item.mve_c_iid);
                    }
                    catch (Exception e)
                    {
                        return Ok(-1);
                    }
                }
            }
            return Ok(-1);
        }
        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getItemsById([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query_item = from A in _context_MS.Set<T_MOVIMIENTO_ENTRADA_DETALLE>()
                                 join B in (
                                     from C in _context_MS.Set<T_MOVIMIENTO_ENTRADA_DETALLE>()
                                     join CC in _context_MS.Set<T_MOVIMIENTO_ENTRADA>() on C.mve_c_iid equals CC.mve_c_iid into DD
                                     from EE in DD.DefaultIfEmpty()
                                     where C.mve_c_iid != id.id && EE.mve_c_iestado == 3
                                     group C by C.mve_c_iocdet_id into D
                                     select new
                                     {
                                         id = D.Key,
                                         sum = D.Sum(x => x.mve_c_ecant_recibida)
                                     }
                                 ) on A.mve_c_iocdet_id equals B.id into E
                                 from F in E.DefaultIfEmpty()
                                 where A.mve_c_iid == id.id
                                 select new
                                 {
                                     item_c_iid = A.item_c_iid,
                                     iocdet_id = A.mve_c_iocdet_id,
                                     pedida = A.mve_c_ecant_pedida,
                                     descripcion = A.mve_c_vdescripcion_item,
                                     recibida = A.mve_c_ecant_recibida,
                                     atendida = F.sum == null ? 0 : F.sum,
                                     maxatendida = Math.Round(A.mve_c_ecant_pedida / 5) + A.mve_c_ecant_pedida
                                 };

                var result_item = query_item.ToList();
                return Ok(result_item);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getAlmacenById([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(id.id)).FirstOrDefault();
                var almacenItem = _context_MS.Almacen.Where(e => e.alm_c_iid.Equals(movimientoItem.mve_c_iidalmacen)).FirstOrDefault();
                return Ok(almacenItem);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getOrdenById([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(id.id)).FirstOrDefault();
                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA>()
                            join B in _context_MS.Set<T_CLIENTE>() on A.prov_c_vdoc_id equals B.cli_c_vdoc_id into C
                            from D in C.DefaultIfEmpty()
                            where A.odc_c_iid == movimientoItem.odc_c_iid
                            select new
                            {
                                serie = A.odc_c_cserie,
                                numero = A.odc_c_iid.ToString().PadLeft(9, '0'),
                                proveedor = D.cli_c_vraz_soc,
                                id = A.odc_c_iid
                            };
                var result = query.FirstOrDefault();
                return Ok(result);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getDetailEntradaById([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(id.id)).FirstOrDefault();
                return Ok(movimientoItem);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult changeToCerrar([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(id.id)).FirstOrDefault();
                movimientoItem.mve_c_iestado = 3;
                movimientoItem.mve_c_vdesestado = "CERRADO";
                movimientoItem.mve_c_bingresado = true;

                _context_MS.Movimiento_entrada.Update(movimientoItem);
                _context_MS.SaveChanges();

                var _detalleItems = _context_MS.Movimiento_entrada_detalle.Where(e => e.mve_c_iid.Equals(id.id)).ToList();
                foreach (var item in _detalleItems)
                {
                    if (item.mve_c_ecant_recibida > 0)
                    {
                        T_ITEM_ALMACEN _item_almacen = new();
                        _item_almacen.alm_c_iid = movimientoItem.mve_c_iidalmacen;
                        _item_almacen.itm_c_iid = item.item_c_iid;
                        _item_almacen.itm_alm_c_ecantidad = item.mve_c_ecant_recibida;
                        _context_MS.Item_almacen.Add(_item_almacen);
                    }
                }
                _context_MS.SaveChanges();
                return Ok(movimientoItem.mve_c_iid);
            }
            else
            {
                return Ok(-1);
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult changeToAnular([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var movimientoItem = _context_MS.Movimiento_entrada.Where(e => e.mve_c_iid.Equals(id.id)).FirstOrDefault();
                movimientoItem.mve_c_iestado = 4;
                movimientoItem.mve_c_vdesestado = "ANULADO";

                _context_MS.Movimiento_entrada.Update(movimientoItem);
                _context_MS.SaveChanges();
                return Ok(movimientoItem.mve_c_iid);
            }
            else
            {
                return Ok(-1);
            }
        }
    }
}

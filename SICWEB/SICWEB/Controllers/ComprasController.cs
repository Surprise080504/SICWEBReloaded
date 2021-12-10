
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
    public class ComprasController : ControllerBase
    {
        private readonly MaintenanceMssqlDbContext _context_MS;
        private readonly string _engine;
        public ComprasController(
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
        public IActionResult getOrdenEstados()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.Odc_estado.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getClase()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.Odc_clase.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getMoneda()
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_PARAMETRO_DET>()
                            where A.par_c_iid == 5
                            select new
                            {
                                A.par_det_c_iid,
                                A.par_det_c_vdesc
                            };

                return Ok(query);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getDirection()
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_EMP_DIRECCION>()
                            select new
                            {
                                id = A.emp_dir_c_iid,
                                label = A.emp_dir_c_vdireccion
                            };
                return Ok(query);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEstado()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.Odc_estado.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getDlvrAddr()
        {
            if (_engine.Equals("MSSQL"))
            {
                return Ok(_context_MS.Odc_clase.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult obtNroSerie()
        {
            int length = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(1)).ToString().Length - 1;
            string serie = String.Concat(Enumerable.Repeat("0", length)) + Convert.ToInt32(DateTime.Now.Year.ToString().Substring(1)).ToString();
            return Ok(serie);

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
                             ) on A.odc_c_ymoneda equals E.par_det_c_iid //into F
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
                                odc_c_iestado = A.odc_c_iestado
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
        public IActionResult deleteOrdens([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var _detailitems = _context_MS.Orden_de_compra_det.Where(u => u.odc_c_iid.Equals(id.id));
                    foreach (var _detailitem in _detailitems)
                    {
                        _context_MS.Orden_de_compra_det.Remove(_detailitem);
                    }
                    _context_MS.SaveChanges();

                    var _item = _context_MS.Orden_de_compra.Where(u => u.odc_c_iid.Equals(id.id)).FirstOrDefault();
                    _context_MS.Orden_de_compra.Remove(_item);
                    _context_MS.SaveChanges();
                    return Ok(true);
                }catch(Exception e)
                {
                    return Ok(false);
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
        public IActionResult saveOrdens([FromBody] NewOrden orden)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (orden.id < 0)
                {
                    try
                    {
                        T_ORDEN_DE_COMPRA _compar = new();
                        var rows = _context_MS.Orden_de_compra.Where(u => u.odc_c_cserie.Equals(orden.serie)).ToArray();
                        int maxNumber = 0;
                        foreach(var data in rows)
                        {
                            int curNum = Int32.Parse(data.odc_c_vcodigo.TrimStart('0'));
                            if(maxNumber < curNum)
                            {
                                maxNumber = curNum;
                            }
                        }
                        string strTmpcode = "00000000" + (maxNumber + 1).ToString();
                        _compar.odc_c_vcodigo = strTmpcode.Substring(strTmpcode.Length - 8, 8);
                        _compar.prov_c_vdoc_id = orden.ruc_proveedor;
                        _compar.odc_c_zfechaentrega_ini = orden.entregastart;
                        _compar.odc_c_zfechaentrega_fin =orden.entregaend;
                        _compar.odc_c_clase_iid = orden.clase;
                        _compar.odc_c_zfechaemi = orden.issuedate;
                        _compar.odc_c_iestado = orden.estado;
                        _compar.odc_c_ymoneda = (byte)orden.moneda;
                        _compar.odc_c_vobservacion = orden.observe;
                        _compar.emp_dir_c_iid = orden.dlvaddr;
                        _compar.odc_c_etotal = orden.total;
                        _compar.odc_c_bpercepcion = orden.percentcheck;
                        _compar.odc_c_cserie = orden.serie;
                        _compar.odc_c_esubtotal = orden.subtotal;
                        _compar.odc_c_eigvcal = orden.igvcal;
                        _compar.odc_c_epercepcioncal = orden.perceptioncal;
                        _compar.odc_c_iid_usuario_creador = User.Identity.Name;

                        _compar.odc_c_vdescestado = orden.vdescestado;
                        _compar.odc_c_vdescmoneda = orden.vdescmoneda;

                        //_compar.odc_c _zfecharmod = DateTime.Now;
                        _compar.odc_c_zfecharegistro = DateTime.Now;
                        _compar.emp_dir_c_iid = orden.dlvaddr;

                        _compar.odc_c_clase_des = "EN PROCESO";
                        

                        _context_MS.Orden_de_compra.Add(_compar);
                        _context_MS.SaveChanges();

                        foreach (var item in orden.items)
                        {
                            T_ORDEN_DE_COMPRA_DET _compar_det = new();
                            _compar_det.odc_c_iid = _compar.odc_c_iid;
                            _compar_det.odc_c_iitemid = item.itm_c_iid;
                            _compar_det.odc_c_ecantidad = item.quantity;
                            _compar_det.odc_c_epreciounit = item.und_c_yid;
                            _compar_det.odc_c_epreciototal = item.quantity * item.und_c_yid;

                            _context_MS.Orden_de_compra_det.Add(_compar_det);
                        }
                        _context_MS.SaveChanges();

                        return Ok(_compar.odc_c_iid);
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
                        var _item = _context_MS.Orden_de_compra.Where(e => e.odc_c_iid.Equals(orden.id)).FirstOrDefault();
                        _item.prov_c_vdoc_id = orden.ruc_proveedor;
                        _item.odc_c_zfechaentrega_ini = Convert.ToDateTime(orden.entregastart);
                        _item.odc_c_zfechaentrega_fin = Convert.ToDateTime(orden.entregaend);
                        _item.odc_c_clase_iid = orden.clase;
                        _item.odc_c_zfechaemi = Convert.ToDateTime(orden.issuedate);
                        _item.odc_c_iestado = orden.estado;
                        _item.odc_c_ymoneda = (byte)orden.moneda;
                        _item.odc_c_vobservacion = orden.observe;
                        _item.emp_dir_c_iid = orden.dlvaddr;
                        _item.odc_c_etotal = orden.total;
                        _item.odc_c_bpercepcion = orden.percentcheck;
                        _item.odc_c_cserie = orden.serie;
                        _item.odc_c_esubtotal = orden.subtotal;
                        _item.odc_c_eigvcal = orden.igvcal;
                        _item.odc_c_epercepcioncal = orden.perceptioncal;
                        _item.odc_c_vdescestado = orden.vdescestado;
                        _item.odc_c_vdescmoneda = orden.vdescmoneda;

                        _context_MS.Orden_de_compra.Update(_item);
                        _context_MS.SaveChanges();

                        var detailItems = _context_MS.Orden_de_compra_det.Where(e => e.odc_c_iid.Equals(orden.id));

                        foreach (var detailItem in detailItems)
                        {
                            bool isDBDataExist = false;
                            foreach (var ordenitem in orden.items)
                            {
                                if (ordenitem.odc_det_c_iid == detailItem.odc_det_c_iid)
                                {
                                    isDBDataExist = true;
                                }
                            }

                            if (!isDBDataExist)
                            {
                                _context_MS.Orden_de_compra_det.Remove(detailItem);
                            }
                        }

                        foreach (var ordenitem in orden.items)
                        {
                            
                            bool isExist = false;
                            foreach (var detailItem in detailItems)
                            {
                                if (ordenitem.odc_det_c_iid == detailItem.odc_det_c_iid)
                                {
                                    isExist = true;
                                }
                            }

                            if (!isExist)
                            {
                                T_ORDEN_DE_COMPRA_DET _compar_det = new();
                                _compar_det.odc_c_iid = _item.odc_c_iid;
                                _compar_det.odc_c_iitemid = ordenitem.itm_c_iid;
                                _compar_det.odc_c_ecantidad = ordenitem.quantity;
                                _compar_det.odc_c_epreciounit = ordenitem.und_c_yid;
                                _compar_det.odc_c_epreciototal = ordenitem.quantity * ordenitem.und_c_yid;

                                _context_MS.Orden_de_compra_det.Add(_compar_det);
                            }
                            else
                            {
                                var _existingItem = _context_MS.Orden_de_compra_det.Where(u => u.odc_det_c_iid.Equals(ordenitem.odc_det_c_iid)).FirstOrDefault();
                                _existingItem.odc_c_iid = _item.odc_c_iid;
                                _existingItem.odc_c_iitemid = ordenitem.itm_c_iid;
                                _existingItem.odc_c_ecantidad = ordenitem.quantity;
                                _existingItem.odc_c_epreciounit = ordenitem.und_c_yid;
                                _existingItem.odc_c_epreciototal = ordenitem.quantity * ordenitem.und_c_yid;

                                _context_MS.Orden_de_compra_det.Update(_existingItem);
                            }
                        }

                        _context_MS.SaveChanges();

                        return Ok(_item.odc_c_iid);
                    }
                    catch(Exception e)
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
        public IActionResult getOrdenDetail([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA>()
                            join F in _context_MS.Set<T_EMP_DIRECCION>() on A.emp_dir_c_iid equals F.emp_dir_c_iid into FF
                            from FFF in FF.DefaultIfEmpty()
                            join G in _context_MS.Set<T_ODC_CLASE>() on A.odc_c_clase_iid equals G.odc_cla_iid into GG
                            from GGG in GG.DefaultIfEmpty()
                            join B in
                            (
                                from AA in _context_MS.Set<T_CLIENTE>()
                                where AA.cli_c_bproveedor == true
                                select new
                                {
                                    AA.cli_c_vdoc_id,
                                    AA.cli_c_vraz_soc
                                }
                            ) on A.prov_c_vdoc_id equals B.cli_c_vdoc_id into C
                            from D in C.DefaultIfEmpty()                        
                            where A.odc_c_iid == id.id
                            select new
                            {
                                proveedor = D.cli_c_vraz_soc,
                                A.emp_dir_c_iid,
                                FFF.emp_dir_c_vdireccion,
                                A.odc_c_bactivo,
                                A.odc_c_bpercepcion,
                                A.odc_c_clase_des,
                                A.odc_c_clase_iid,
                                GGG.odc_cla_vdes,
                                A.odc_c_cserie,
                                A.odc_c_eigv,
                                A.odc_c_eigvcal,
                                A.odc_c_epercepcion,
                                A.odc_c_epercepcioncal,
                                A.odc_c_esubtotal,
                                A.odc_c_etotal,
                                A.odc_c_iestado,
                                A.odc_c_iid,
                                A.odc_c_iid_usuario_creador,
                                A.odc_c_iid_usuario_mod,
                                A.odc_c_vcodigo,
                                A.odc_c_vdescestado,
                                A.odc_c_vdescmoneda,
                                A.odc_c_vobservacion,
                                A.odc_c_ymoneda,
                                A.odc_c_zfechaemi,
                                A.odc_c_zfechaentrega_fin,
                                A.odc_c_zfechaentrega_ini,
                                A.odc_c_zfecharegistro,
                                A.odc_c_zfecharmod,
                                A.prov_c_vdoc_id
                            };

                return Ok(query.FirstOrDefault());
            }
            return Ok(-1);
        }
        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getOrdenItems([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA_DET>()
                            join B in _context_MS.Set<T_ITEM>() on A.odc_c_iitemid equals B.itm_c_iid
                            join C in _context_MS.Set<T_UNIDAD_MEDIDA>() on B.und_c_yid equals C.und_c_yid into D
                            from F in D.DefaultIfEmpty()
                            select new
                            {
                                A.odc_c_iid,
                                itm_c_iid = A.odc_c_iitemid,
                                B.itm_c_ccodigo,
                                B.itm_c_vdescripcion,
                                quantity = A.odc_c_ecantidad,
                                und_c_yid = A.odc_c_epreciounit,
                                odc_det_c_iid = A.odc_det_c_iid,
                                checkstate = false,
                                F.und_c_vdesc,
                                A.odc_c_epreciototal
                            };

                query = query.Where(u => u.odc_c_iid.Equals(id.id));

                return Ok(query);
            }
            return Ok(-1);
        }

        //private static PdfPCell Celdas(PdfPTable pdfTable, string texto = " ", int border = Rectangle.BOX)
        //{
        //    return new PdfPCell(new Phrase(texto)) { Colspan = pdfTable.NumberOfColumns, Border = border };

        //}

        //private static PdfPCell Celdas(int numeroDeColumnas, string texto = " ", int border = Rectangle.BOX)
        //{
        //    return new PdfPCell(new Phrase(texto)) { Colspan = numeroDeColumnas, Border = border };

        //}

        //public static void init(SIC_T_ORDEN_DE_COMPRA ordenCompra)
        //{

        //    Document doc = new Document();
        //    MemoryStream memStream = new MemoryStream();
        //    PdfWriter pdf = PdfWriter.GetInstance(doc, memStream);
        //    pdf.PageEvent = new MyPageEventHandler();
        //    doc.Open();

        //    iTextSharp.text.Font fontTitulo = FontFactory.GetFont("Calibri", 12, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        //    iTextSharp.text.Font fontSubtitulo = FontFactory.GetFont("Calibri", 9, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        //    iTextSharp.text.Font fontPropiedad = FontFactory.GetFont("Calibri", 8, Font.BOLD, iTextSharp.text.BaseColor.BLACK);
        //    iTextSharp.text.Font fontInfo = FontFactory.GetFont("Calibri", 8, iTextSharp.text.BaseColor.BLACK);


        //    PdfPTableHeader th = new PdfPTableHeader();
        //    PdfPTableBody tb = new PdfPTableBody();
        //    PdfPTableFooter tf = new PdfPTableFooter();

        //    PdfPTable t = new PdfPTable(new[] { 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f });
        //    t.WidthPercentage = 100;

        //    t.AddCell(Celdas(t, border: Rectangle.NO_BORDER));

        //    t.AddCell(Celdas(2, border: Rectangle.NO_BORDER));

        //    t.AddCell(new PdfPCell(new Phrase("ORDEN DE COMPRA #" + ordenCompra.odc_c_cserie + " - " + ordenCompra.odc_c_vcodigo, fontTitulo)) { Colspan = 6, Border = Rectangle.NO_BORDER, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });

        //    t.AddCell(Celdas(2, border: Rectangle.NO_BORDER));

        //    t.AddCell(Celdas(10, border: Rectangle.NO_BORDER));

        //    t.AddCell(new PdfPCell(new Phrase("DATOS DEL PROVEEDOR", fontSubtitulo)) { Colspan = 6, Border = 0 });

        //    t.AddCell(new PdfPCell(new Phrase("FECHA DE EMISIÓN", fontSubtitulo)) { Colspan = 2, Border = 0, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });
        //    t.AddCell(new PdfPCell(new Phrase(String.Format("{0:dd/MM/yyyy}", ordenCompra.odc_c_zfechaemi), fontInfo)) { Colspan = 2, Border = Rectangle.NO_BORDER, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });


        //    t.AddCell(new PdfPCell(new Phrase("PROVEEDOR", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.SIC_T_CLIENTE.cli_c_vraz_soc, fontInfo)) { Colspan = 5 });

        //    t.AddCell(new PdfPCell(new Phrase("RUC", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.SIC_T_CLIENTE.cli_c_vdoc_id, fontInfo)) { Colspan = 2 });

        //    List<SIC_T_CLI_CONTACTO> lstProvContacto = ordenCompra.SIC_T_CLIENTE.SIC_T_CLI_CONTACTO.ToList<SIC_T_CLI_CONTACTO>();
        //    String provContacto = lstProvContacto[0].cli_contac_c_vape_pat + ", " + lstProvContacto[0].cli_contac_c_vnomb;

        //    t.AddCell(new PdfPCell(new Phrase("CONTACTO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(provContacto, fontInfo)) { Colspan = 4 });

        //    SIC_T_CLI_DIRECCION cliDirec = ordenCompra.SIC_T_CLIENTE.SIC_T_CLI_DIRECCION.ToList<SIC_T_CLI_DIRECCION>()
        //        .Where(x => x.cli_direc_c_ctipo == Convert.ToInt32(TipoDirecCliente.FISCAL).ToString()).ToList()[0];

        //    String direc = "";
        //    if (cliDirec != null)
        //    {
        //        direc = cliDirec.cli_direc_c_vdirec + " " + cliDirec.SIC_T_DISTRITO.SIC_T_PROVINCIA.SIC_T_DEPARTAMENTO.depa_c_vnomb +
        //            " " + cliDirec.SIC_T_DISTRITO.SIC_T_PROVINCIA.prov_c_vnomb + " " + cliDirec.SIC_T_DISTRITO.dist_c_vnomb;
        //    }

        //    t.AddCell(new PdfPCell(new Phrase("DIRECCIÓN", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(direc, fontInfo)) { Colspan = 4 });

        //    t.AddCell(new PdfPCell(new Phrase("TELÉFONO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.SIC_T_CLIENTE.cli_c_ctlf, fontInfo)) { Colspan = 2 });

        //    t.AddCell(new PdfPCell(new Phrase("FAX", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase("", fontInfo)));

        //    t.AddCell(new PdfPCell(new Phrase("USUARIO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_iid_usuario_creador, fontInfo)) { Colspan = 4 });


        //    t.AddCell(Celdas(t, border: Rectangle.NO_BORDER));

        //    t.AddCell(new PdfPCell(new Phrase("DATOS GENERALES", fontSubtitulo)) { Colspan = 2, Border = Rectangle.NO_BORDER });
        //    t.AddCell(Celdas(8, border: Rectangle.NO_BORDER));

        //    t.AddCell(new PdfPCell(new Phrase("FECHA ENTREGA", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_zfechaentrega_ini.ToString("dd/MM/yyyy") + " al  "
        //                                    + ordenCompra.odc_c_zfechaentrega_fin.ToString("dd/MM/yyyy"), fontInfo))
        //    { Colspan = 4 });

        //    t.AddCell(new PdfPCell(new Phrase("CLASE DE O/C", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_clase_des, fontInfo)) { Colspan = 4 });


        //    t.AddCell(new PdfPCell(new Phrase("COND. PAGO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase("", fontInfo)) { Colspan = 4 });

        //    t.AddCell(new PdfPCell(new Phrase("LUGAR ENTREGA", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.SIC_T_EMP_DIRECCION.emp_dir_c_vdireccion, fontInfo)) { Colspan = 4 });

        //    t.AddCell(new PdfPCell(new Phrase("PROCESO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase("", fontInfo)) { Colspan = 4 });

        //    t.AddCell(new PdfPCell(new Phrase("GRUPO REQUERIDO", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase("", fontInfo)) { Colspan = 4 });


        //    t.AddCell(Celdas(t, border: Rectangle.NO_BORDER));

        //    //aquí el detalle de OC

        //    t.AddCell(new PdfPCell(new Phrase("DETALLE", fontSubtitulo)) { Colspan = 2, Border = Rectangle.NO_BORDER });
        //    t.AddCell(Celdas(8, border: Rectangle.NO_BORDER));

        //    t.AddCell(new PdfPCell(new Phrase("CANT. REQ.", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("UNI. MEDIDA", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("COD. ITEM", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("DESCRIPCIÓN ITEM", fontPropiedad)) { Colspan = 4, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("P.U.", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("DSCTO", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //    t.AddCell(new PdfPCell(new Phrase("TOTAL", fontPropiedad)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });


        //    foreach (SIC_T_ORDEN_DE_COMPRA_DET det in ordenCompra.SIC_T_ORDEN_DE_COMPRA_DET)
        //    {
        //        t.AddCell(new PdfPCell(new Phrase(String.Format("{0:0.00}", det.odc_c_ecantidad), fontInfo)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //        t.AddCell(new PdfPCell(new Phrase(det.SIC_T_ITEM.SIC_T_UNIDAD_MEDIDA.und_c_vdesc, fontInfo)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Center) });
        //        t.AddCell(new PdfPCell(new Phrase(det.SIC_T_ITEM.itm_c_ccodigo, fontInfo)));
        //        t.AddCell(new PdfPCell(new Phrase(det.SIC_T_ITEM.itm_c_vdescripcion, fontInfo)) { Colspan = 4 });
        //        t.AddCell(new PdfPCell(new Phrase(String.Format("{0:0,0.00}", det.odc_c_epreciounit), fontInfo)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });
        //        t.AddCell(new PdfPCell(new Phrase("0.00", fontInfo)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });
        //        t.AddCell(new PdfPCell(new Phrase(String.Format("{0:0,0.00}", det.odc_c_epreciototal), fontInfo)) { HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });

        //    }

        //    t.AddCell(Celdas(t));

        //    t.AddCell(new PdfPCell(new Phrase("MONEDA", fontPropiedad)));
        //    t.AddCell(new PdfPCell(new Phrase(Enum.GetName(typeof(TipoMonedaDescripcion), ordenCompra.odc_c_ymoneda).Replace('_', ' ')
        //        , fontInfo))
        //    { Colspan = 9 });
        //    //t.AddCell(Celdas(9));


        //    t.AddCell(Celdas(t));

        //    t.AddCell(new PdfPCell(new Phrase("OP", fontPropiedad)));

        //    t.AddCell(new PdfPCell(new Phrase("", fontInfo)) { Colspan = 5 });
        //    t.AddCell(new PdfPCell(new Phrase("VALOR VENTA", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_esubtotal.ToString(), fontInfo)) { Colspan = 2, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });


        //    t.AddCell(Celdas(6));
        //    t.AddCell(new PdfPCell(new Phrase("I.G.V.", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_eigvcal.ToString(), fontInfo)) { Colspan = 2, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });

        //    t.AddCell(Celdas(6));
        //    t.AddCell(new PdfPCell(new Phrase("PERCEPCIÓN", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_epercepcioncal.ToString(), fontInfo)) { Colspan = 2, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });

        //    t.AddCell(Celdas(6));
        //    t.AddCell(new PdfPCell(new Phrase("TOTAL", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(ordenCompra.odc_c_etotal.ToString(), fontInfo)) { Colspan = 2, HorizontalAlignment = Convert.ToInt32(eTextAlignment.Right) });

        //    t.AddCell(Celdas(t));

        //    t.AddCell(new PdfPCell(new Phrase("OBSERVACIÓN", fontPropiedad)) { Colspan = 10 });

        //    t.AddCell(Celdas(t));

        //    t.AddCell(Celdas(t, ordenCompra.odc_c_vobservacion));

        //    t.AddCell(Celdas(t));


        //    t.AddCell(new PdfPCell(new Phrase("VoBo Gerencia", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 3 });

        //    t.AddCell(new PdfPCell(new Phrase("VoBo Emisor", fontPropiedad)) { Colspan = 2 });
        //    t.AddCell(new PdfPCell(new Phrase(" ")) { Colspan = 3 });

        //    t.AddCell(Celdas(t));



        //    t.AddCell(new PdfPCell(new Phrase("NOTAS", fontPropiedad)) { Colspan = 10, Border = Rectangle.NO_BORDER });
        //    List listNotas = new List(List.UNORDERED, 15f);
        //    listNotas.PreSymbol = "-";


        //    listNotas.Add(new ListItem("Despachar solo cantidades solicitadas. No se aceptan excesos.", fontInfo));
        //    listNotas.Add(new ListItem("Recoger O/C original y al despachar la mercaderia, adjuntar fotocopia autenticada de la misma, indicando en ella el N° de entrega correspondiente", fontInfo));
        //    listNotas.Add(new ListItem("Enviar la factura maximo a los 3 dias de haber entregado cada guia", fontInfo));
        //    listNotas.Add(new ListItem("Los pagos se realizarán a nombre de la razón social que fugura en la Factura y en la Orden de Compra", fontInfo));


        //    //t.AddCell(Celdas(t, "SUMIT SAC - RUC:20431991960 / Direccion:Calle A Mz.B Lote 8-D Zona Industrial Bocanegra-Callao / Tlf:574-7272 / Fax:484-5589"));            

        //    doc.Add(t);

        //    doc.Add(listNotas);

        //    //PdfPTable t2 = new PdfPTable(10);
        //    ////t2.WidthPercentage = 80;
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));
        //    //t2.AddCell(new PdfPCell(new Paragraph("carajo...")));

        //    //doc.Add(t2);


        //    doc.Close();

        //    if (memStream != null)
        //    {
        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.AppendHeader("Content-Disposition", "Attachment; filename=ORDEN_DE_COMPRA.pdf");
        //        HttpContext.Current.Response.ContentType = "application/pdf";
        //        HttpContext.Current.Response.OutputStream.Write(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
        //        HttpContext.Current.Response.OutputStream.Flush();
        //        memStream.Close();
        //        HttpContext.Current.Response.End();
        //    }

        //}

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Download()
        {
            if (_engine.Equals("MSSQL"))
            {
                await Task.Yield();

                var query = from A in _context_MS.Set<T_ORDEN_DE_COMPRA>()
                            join B in _context_MS.Set<T_ODC_ESTADO>() on A.odc_c_iestado equals B.odc_estado_iid into C
                            from D in C.DefaultIfEmpty()
                            join E in
                             (
                                 from AA in _context_MS.Set<T_PARAMETRO_DET>()
                                 where AA.par_c_iid == 5
                                 select new
                                 {
                                     AA.par_det_c_iid,
                                     AA.par_det_c_vdesc
                                 }
                             ) on A.odc_c_ymoneda equals E.par_det_c_iid into F
                            from G in F.DefaultIfEmpty()
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
                                codigo = A.odc_c_vcodigo,
                                ruc = A.prov_c_vdoc_id,
                                prov = H.cli_c_vraz_soc,
                                estado = D.odc_estado_vdescripcion,
                                moneda = G.par_det_c_vdesc == null ? "" : G.par_det_c_vdesc,
                                monedaid = G.par_det_c_iid == null ? 0 : G.par_det_c_iid,
                                monototal = A.odc_c_etotal,
                                odc_c_iestado = A.odc_c_iestado
                            };

                var result = query.ToList();


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

            }
            else
            {
                return NotFound();
            }
        }

    }
}

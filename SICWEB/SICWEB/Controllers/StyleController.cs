
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
    public class StyleController : ControllerBase
    {
        private readonly ConfeccionMssqlDbContext _context_MS;
        private readonly MaintenanceMssqlDbContext _context_MS2;
        private readonly string _engine;
        [Obsolete]
        private IHostingEnvironment Environment;

        [Obsolete]
        public StyleController(
            ConfeccionMssqlDbContext context_MS,
            MaintenanceMssqlDbContext context_MS2,
            IConfiguration configuration,
            IHostingEnvironment _environment
        )
        {
            _context_MS = context_MS;
            _context_MS2 = context_MS2;
            // _context_MS3 = context_MS3;
            _engine = configuration.GetConnectionString("ActiveEngine");
            Environment = _environment;

        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allBrands()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.MARCA.ToArray());
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
        public IActionResult getColor([FromBody] IdKey2 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.MARCA_COLOR.Where(u => u.marca_c_vid.Equals(id.id)).ToArray();

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
        public IActionResult getEditAccess([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = (from A in _context_MS.PEDIDO_DETALLE
                              join B in _context_MS.ESTILO_TALLA on A.estilo_talla_c_iid equals B.estilo_talla_c_iid
                              where B.estilo_c_iid == id.id
                              select 1).Count();

                if (result > 0)
                {
                    return Ok(true);
                }
                else
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
        public IActionResult getCategory([FromBody] IdKey2 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                var result = _context_MS.MARCA_CATEGORIA.Where(u => u.marca_c_vid.Equals(id.id)).ToArray();

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
        public IActionResult getOriginCategory()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.CATEGORIA.ToArray());
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
        public IActionResult SaveCategory([FromBody] NewCategory newCategory)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.MARCA_CATEGORIA.Where(u => u.marca_categoria_c_vid.Equals(newCategory.category)).Any())
                    return Conflict();
                T_MARCA_CATEGORIA _category = new();
                _category.marca_categoria_c_vid = newCategory.category;
                _category.marca_c_vid = newCategory.brand;
                _category.categoria_c_vid = newCategory.category_m;
                _category.Marca_categoria_c_vmaterial = newCategory.material;
                _category.Marca_categoria_c_vproceso = newCategory.process;
                _context_MS.MARCA_CATEGORIA.Add(_category);
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
        public IActionResult SaveCategoryDetail([FromBody] NewCategoryDetail value)
        {
            if (_engine.Equals("MSSQL"))
            {
                T_CATEGORIA _category = new();
                _category.categoria_c_vid = value.category;
                _category.categoria_c_vdescripcion = value.description;
                _context_MS.CATEGORIA.Add(_category);
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
        public IActionResult SaveMarca([FromBody] NewMarca value)
        {
            if (_engine.Equals("MSSQL"))
            {
                T_MARCA _marca = new();
                _marca.marca_c_vid = value.marca;
                _marca.marca_c_vdescripcion = value.description;
                _context_MS.MARCA.Add(_marca);
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
        public IActionResult SaveColor([FromBody] NewColor newColor)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.MARCA_COLOR.Where(u => u.marca_color_c_vid.Equals(newColor.color_m)).Any())
                    return Conflict(new { message = "Color de Marca ya existe." });

                if (!_context_MS.COLOR.Where(u => u.color_c_vid.Equals(newColor.color)).Any())
                {
                    T_COLOR _baseColor = new();
                    _baseColor.color_c_vid = newColor.color;
                    _baseColor.color_c_vdescripcion = newColor.colorName;
                    _context_MS.COLOR.Add(_baseColor);
                    _context_MS.SaveChanges();
                }

                T_MARCA_COLOR _color = new();
                _color.marca_color_c_vid = newColor.color_m;
                _color.marca_color_c_vcodigo = newColor.code;
                _color.marca_color_c_vdescripcion = newColor.description;
                _color.marca_c_vid = newColor.brand;
                _color.color_c_vid = newColor.color;
                _color.marca_color_c_bpropio = newColor.own;
                _context_MS.MARCA_COLOR.Add(_color);
                _context_MS.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult allOriginColors()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.COLOR.ToArray());
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
        public IActionResult allColors()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.COLOR.ToArray());
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
        public IActionResult allCategories()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.CATEGORIA.ToArray());
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
        public IActionResult allTallas()
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    return Ok(_context_MS.TALLA.ToArray());
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
        public IActionResult getEstiloTallaID([FromBody] IdKey5 id)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(int.Parse(id.id1))).Where(u => u.talla_c_vid.Equals(id.id2)).FirstOrDefault();
                    return Ok(query != null ? query.estilo_talla_c_iid : -1);
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
        public IActionResult getCurTallas([FromBody] IdKey id)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(id.id)).ToArray();
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

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getStyle([FromBody] SearchStyleKey searchStyleKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_ESTILO>()
                            join C in _context_MS.Set<T_MARCA>()
                            on A.marca_c_vid equals C.marca_c_vid
                            join D in _context_MS.Set<T_MARCA_CATEGORIA>()
                            on A.marca_categoria_c_vid equals D.marca_categoria_c_vid
                            join E in _context_MS.Set<T_MARCA_COLOR>()
                            on A.marca_color_c_vid equals E.marca_color_c_vid
                            select new
                            {
                                A.estilo_c_iid,
                                A.estilo_c_vcodigo,
                                A.estilo_c_vnombre,
                                A.estilo_c_vdescripcion,
                                A.itm_c_iid,
                                A.marca_c_vid,
                                A.marca_categoria_c_vid,
                                A.marca_color_c_vid,
                                brandName = C.marca_c_vdescripcion,
                                categoryName = D.marca_categoria_c_vid,
                                colorName = E.marca_color_c_vid,
                                marcaColorDescription = E.marca_color_c_vdescripcion
                            };


                if (!(searchStyleKey.code == null) && !searchStyleKey.code.Equals(""))
                {
                    query = query.Where(a => a.estilo_c_vcodigo.Contains(searchStyleKey.code));

                }
                if (!(searchStyleKey.name == null) && !searchStyleKey.name.Equals(""))
                {
                    query = query.Where(a => a.estilo_c_vnombre.Contains(searchStyleKey.name));

                }
                if (!(searchStyleKey.color == null) && !searchStyleKey.color.Equals(""))
                {
                    query = query.Where(a => a.colorName.Contains(searchStyleKey.color));

                }

                query = query.Where(a => a.marcaColorDescription != null).Distinct();

                var query2 = from A in _context_MS2.Set<T_ITEM>()
                             select new
                             {
                                 A.itm_c_iid,
                                 itemName = A.itm_c_ccodigo
                             };
                var res = new List<Style2>();
                foreach (var i in query)
                {
                    Style2 _s = new();
                    _s.estilo_c_iid = i.estilo_c_iid;
                    _s.estilo_c_vcodigo = i.estilo_c_vcodigo;
                    _s.estilo_c_vnombre = i.estilo_c_vnombre;
                    _s.estilo_c_vdescripcion = i.estilo_c_vdescripcion;
                    _s.itm_c_iid = i.itm_c_iid;
                    _s.marca_c_vid = i.marca_c_vid;
                    _s.marca_categoria_c_vid = i.marca_categoria_c_vid;
                    _s.marca_color_c_vid = i.marca_color_c_vid;
                    _s.marcaColorDescription = i.marcaColorDescription;
                    _s.brandName = i.brandName;
                    _s.categoryName = i.categoryName;
                    _s.colorName = i.colorName;
                    var _sizeName = _context_MS.ESTILO_TALLA.Where(e => e.estilo_c_iid.Equals(i.estilo_c_iid)).ToArray();

                    var _sizeList = new List<SizeArray>();
                    foreach (var j in _sizeName)
                    {
                        SizeArray _sa = new();
                        _sa.key = j.talla_c_vid;
                        _sa.check = true;
                        _sizeList.Add(_sa);
                    }
                    _s.sizeName = _sizeList;
                    _s.itemName = query2.Where(a => a.itm_c_iid.Equals(i.itm_c_iid)).First().itemName;
                    res.Add(_s);
                }

                return Ok(res);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getStyle2([FromBody] SearchStyleKey searchStyleKey)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.ESTILO
                            join B in _context_MS.ESTILO_TALLA
                            on A.estilo_c_iid equals B.estilo_c_iid
                            //join F in _context_MS2.ITEM 
                            //on A.itm_c_iid equals F.itm_c_iid
                            join C in _context_MS.MARCA
                            on A.marca_c_vid equals C.marca_c_vid
                            join D in _context_MS.MARCA_CATEGORIA
                            on A.marca_categoria_c_vid equals D.marca_categoria_c_vid
                            join E in _context_MS.MARCA_COLOR
                            on A.marca_color_c_vid equals E.marca_color_c_vid
                            select new
                            {
                                A.estilo_c_iid,
                                A.estilo_c_vcodigo,
                                A.estilo_c_vnombre,
                                A.estilo_c_vdescripcion,

                                A.itm_c_iid,
                                //F.itm_c_vdescripcion,

                                A.marca_c_vid,
                                A.marca_categoria_c_vid,
                                A.marca_color_c_vid,

                                brandName = C.marca_c_vdescripcion,
                                categoryName = D.marca_categoria_c_vid,
                                colorName = E.marca_color_c_vid,
                                marcaColorDescription = E.marca_color_c_vdescripcion,

                                B.estilo_talla_c_iid,
                                B.talla_c_vid
                            };

                query = query.Where(a => a.marcaColorDescription != null).Distinct();
                var query2 = from A in query.ToList()
                             join B in _context_MS2.ITEM
                             on A.itm_c_iid equals B.itm_c_iid
                             select new Style2()
                             {
                                 estilo_c_vcodigo = A.estilo_c_vcodigo,
                                 estilo_c_vnombre = A.estilo_c_vnombre,
                                 estilo_c_vdescripcion = A.estilo_c_vdescripcion,
                                 itm_c_iid = A.itm_c_iid,
                                 marca_c_vid = A.marca_c_vid,
                                 marca_categoria_c_vid = A.marca_categoria_c_vid,
                                 marca_color_c_vid = A.marca_color_c_vid,
                                 marcaColorDescription = A.marcaColorDescription,
                                 brandName = A.brandName,
                                 categoryName = A.categoryName,
                                 colorName = A.colorName,
                                 itemName = B.itm_c_vdescripcion,
                                 estilo_c_iid = A.estilo_c_iid,
                                 estilo_talla_c_iid = A.estilo_talla_c_iid,
                                 estilo_talla_c_vid = A.talla_c_vid
                             };



                return Ok(query2.ToArray());
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult saveStyle([FromBody] NewStyle style)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (style.id < 0)
                {
                    try
                    {
                        T_ESTILO item = new();
                        item.estilo_c_vcodigo = style.code;
                        item.estilo_c_vnombre = style.name;
                        item.estilo_c_vdescripcion = style.description;
                        item.itm_c_iid = style.item;
                        item.marca_c_vid = style.brand;
                        item.marca_categoria_c_vid = style.category;
                        item.marca_color_c_vid = style.color;
                        _context_MS.ESTILO.Add(item);
                        _context_MS.SaveChanges();

                        for (int i = 0; i < style.sizes.Length; i++)
                        {
                            if (style.sizes[i].check == true)
                            {
                                T_ESTILO_TALLA _item = new();
                                _item.talla_c_vid = style.sizes[i].key;
                                _item.estilo_c_iid = item.estilo_c_iid;
                                _context_MS.ESTILO_TALLA.Add(_item);
                                _context_MS.SaveChanges();
                            }
                        }
                        return Ok(item.estilo_c_iid);
                    }
                    catch (Exception e)
                    {
                        return Conflict(new { message = "" });
                    }
                }
                else
                {
                    try
                    {
                        using (var transaction = _context_MS.Database.BeginTransaction())
                        {
                            var _item = _context_MS.ESTILO.Where(e => e.estilo_c_iid.Equals(style.id)).FirstOrDefault();
                            _item.estilo_c_vcodigo = style.code;
                            _item.estilo_c_vnombre = style.name;
                            _item.estilo_c_vdescripcion = style.description;
                            _item.itm_c_iid = style.item;
                            _item.marca_c_vid = style.brand;
                            _item.marca_categoria_c_vid = style.category;
                            _item.marca_color_c_vid = style.color;
                            _context_MS.ESTILO.Update(_item);
                            _context_MS.SaveChanges();

                            //left join attemp - dont erase
                            //var query3 = from B in _context_MS.ESTILO_TALLA.ToList().DefaultIfEmpty()
                            //             join A in style.sizes
                            //            on B.talla_c_vid equals A.key
                            //            //select B
                            //            into xxxx
                            //             from pco in xxxx.DefaultIfEmpty()
                            //                 //where (pco.estilo_c_iid == style.id || pco.estilo_c_iid ==) && A.check;
                            //             where pco.check && B.estilo_c_iid == style.id
                            //             select new { B, pco };

                            //to add
                            var query = style.sizes.Where(x => !_context_MS.ESTILO_TALLA
                            .Any(y => y.talla_c_vid == x.key && y.estilo_c_iid == style.id) && x.check).ToList();

                            //to remove
                            var query2 = _context_MS.ESTILO_TALLA.ToList().Where(x => !style.sizes.Where(y => y.check)
                            .Any(z => z.key == x.talla_c_vid) && x.estilo_c_iid == style.id).ToList();

                            foreach (var q in query)
                            {
                                T_ESTILO_TALLA __item = new();
                                __item.talla_c_vid = q.key;
                                __item.estilo_c_iid = _item.estilo_c_iid;
                                _context_MS.ESTILO_TALLA.Update(__item);
                                _context_MS.SaveChanges();
                            }

                            foreach (var q in query2)
                            {
                                if (_context_MS.PEDIDO_DETALLE.Where(x => x.estilo_talla_c_iid == q.estilo_talla_c_iid).FirstOrDefault() != null)
                                {
                                    return Conflict(new { message = "La Talla " + q.talla_c_vid + " está en un Pedido, no se puede eliminar." });
                                }

                                if (_context_MS.ESTILO_PROCESO.Where(x => x.estilo_talla_c_iid == q.estilo_talla_c_iid).FirstOrDefault() != null)
                                {
                                    return Conflict(new { message = "La Talla " + q.talla_c_vid + " ya tiene al menos un Proceso asignado, no se puede eliminar." });
                                }

                                if (_context_MS.ESTILO_INSUMO.Where(x => x.estilo_talla_c_iid == q.estilo_talla_c_iid).FirstOrDefault() != null)
                                {
                                    return Conflict(new { message = "La Talla " + q.talla_c_vid + " ya tiene al menos un Insumo asignado, no se puede eliminar." });
                                }

                                var e = _context_MS.ESTILO_TALLA.Where(x => x.estilo_talla_c_iid == q.estilo_talla_c_iid).FirstOrDefault();

                                _context_MS.ESTILO_TALLA.Remove(e);
                                _context_MS.SaveChanges();
                            }

                            transaction.Commit();
                        }
                        return Ok(style.id);
                    }
                    catch (Exception e)
                    {
                        return Conflict(new { message = "" });
                    }
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
        public IActionResult DeleteStyle([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                //if (_context_MS.ITEM_SUB_FAMILIA.Where(u => u.isf_c_vdesc.Equals(newSubFamily.subfamily) && u.isf_c_ifm_iid.Equals(newSubFamily.family)).Any())
                //    return Ok();
                //return Task.FromResult(Ok(_context_MS.UNIDAD_MEDIDA.ToArray()));
                var _isUsed =
                    (from a in _context_MS.ESTILO_TALLA
                     join b in _context_MS.PEDIDO_DETALLE
                     on a.estilo_talla_c_iid equals b.estilo_talla_c_iid
                     where a.estilo_c_iid == item.id
                     select b).Any();

                if (_isUsed)
                {
                    return Ok(false);
                }
                var _item = _context_MS.ESTILO.Where(u => u.estilo_c_iid.Equals(item.id)).FirstOrDefault();
                if (!(_item == null))
                {
                    var _dItem = _context_MS.ESTILO_TALLA.Where(u => u.estilo_c_iid.Equals(_item.estilo_c_iid)).ToList();

                    _context_MS.ESTILO_TALLA.RemoveRange(_dItem);
                    _context_MS.SaveChanges();

                    _context_MS.ESTILO.Remove(_item);
                    _context_MS.SaveChanges();

                    imageDeleteAsync(item.id.ToString());
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

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getEstiloCombList([FromBody] SearchPedidoKey k)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = from A in _context_MS.Set<T_ESTILO>()
                                select new
                                {
                                    value = A.estilo_c_iid,
                                    label = A.estilo_c_vcodigo,
                                    //description = A.estilo_c_vdescripcion,
                                    //iid = A.itm_c_iid
                                };
                    //if (k.id > 0) query = query.Where(u => u.iid.Equals(k.id));
                    return Ok(query);
                }
                catch (Exception e)
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
        public IActionResult getSizeList([FromBody] IdKey k)
        {
            if (_engine.Equals("MSSQL"))
            {
                try
                {
                    var query = from A in _context_MS.Set<T_ESTILO_TALLA>()
                                where A.estilo_c_iid == k.id
                                select new
                                {
                                    value = A.estilo_talla_c_iid,
                                    label = A.talla_c_vid,
                                };

                    return Ok(query);
                }
                catch (Exception e)
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

                using (var stream = new FileStream(Path.Combine(uploadPath, "estilo_style_image_" + _id + ".png"), FileMode.Create))
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
        [Obsolete]
        public async Task<IActionResult> imageDeleteAsync(String _id)
        {
            if (_engine.Equals("MSSQL"))
            {
                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                var uploadDirectory = "ClientApp/public/uploads/";
                var uploadPath = Path.Combine(contentPath, uploadDirectory);

                string filepath = Path.Combine(uploadPath, "estilo_style_image_" + _id + ".png");

                FileInfo file = new FileInfo(filepath);
                if (file.Exists)
                {
                    file.Delete();
                }
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

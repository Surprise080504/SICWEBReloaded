
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
    public class MenuController : ControllerBase
    {
        private readonly MainMssqlDbContext _context_SS;
        private readonly string _engine;
        public MenuController(
            MainMssqlDbContext context_SS,
            IConfiguration configuration
        )
        {
            _context_SS = context_SS;
            _engine = configuration.GetConnectionString("ActiveEngine");

        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Menus([FromBody] SearchMenuKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_SS.Set<T_MENU>()
                            join E in _context_SS.Set<T_MENU>() on A.Menu_c_iid_padre equals E.Menu_c_iid into Detail
                            from M in Detail.DefaultIfEmpty()
                            join O in (
                                from G in _context_SS.Set<T_MENU_OPCION>()
                                group G by G.Menu_c_iid into grouping
                                select new
                                {
                                    menu_c_iid = grouping.Key,
                                    count = grouping.Count()
                                }
                            ) on A.Menu_c_iid equals O.menu_c_iid into Opc
                            from F in Opc.DefaultIfEmpty()
                            select new
                            {
                                menu_c_iid = A.Menu_c_iid,
                                parent_menu_c_iid = A.Menu_c_iid_padre == null ? -1 : A.Menu_c_iid_padre,
                                menu_c_vnomb = A.Menu_c_vnomb,
                                parent_menu_c_vnomb = M.Menu_c_vnomb,
                                menu_c_ynivel = A.Menu_c_ynivel,
                                menu_c_vpag_asp = A.Menu_c_vpag_asp,
                                opciones = F.count == null ? 0 : 1,
                                estado = A.Menu_c_bestado// == true ? 1 : 0
                            };

                if (!(searchKey.menu == null) && !searchKey.menu.Equals(""))
                {
                    query = query.Where(c => c.menu_c_vnomb.Contains(searchKey.menu));

                }
                if (!(searchKey.parent_id == -1))
                {
                    query = query.Where(c => c.parent_menu_c_iid.Equals(searchKey.parent_id));
                }

                if (!(searchKey.state == -1))
                {
                    query = query.Where(c => c.estado.Equals(searchKey.state == 1 ? true : false));
                }

                return Ok(query);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Parentmenus()
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_SS.Set<T_MENU>()
                            where A.Menu_c_ynivel == 1
                            select new
                            {
                                A.Menu_c_iid,
                                A.Menu_c_vnomb
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
        public IActionResult Savemenu([FromBody] NewMenu menu)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (menu.id < 0)
                {
                    //using (_context_SS.Database.BeginTransaction())
                    //{
                    T_MENU _menu = new();
                    if (menu.parent_id != -1)
                    {
                        _menu.Menu_c_iid_padre = menu.parent_id;
                    }
                    _menu.Menu_c_vnomb = menu.menu;
                    _menu.Menu_c_ynivel = menu.nivel;
                    _menu.Menu_c_vpag_asp = menu.pagina;
                    _menu.Menu_c_bestado = menu.estado;
                    _context_SS.Menu.Add(_menu);
                    _context_SS.SaveChanges();

                    var _menuopcs = _context_SS.MenuOpc.Where(e => e.Menu_c_iid.Equals(_menu.Menu_c_iid));
                    foreach (var _menuopc in _menuopcs)
                    {
                        var _menuprofile = _context_SS.Profile_menuopcion.Where(e => e.Menu_opcion_c_iid.Equals(_menuopc.Menu_opcion_c_iid)).FirstOrDefault();
                        if (!(_menuprofile == null))
                        {
                            _context_SS.Profile_menuopcion.Remove(_menuprofile);
                        }
                    }

                    _context_SS.SaveChanges();

                    foreach (var _menuopc in _menuopcs)
                    {

                        _context_SS.MenuOpc.Remove(_menuopc);
                    }
                    _context_SS.SaveChanges();

                    if (menu.opciones is not null)
                    {
                        foreach (NewOpcs _opcdata in menu.opciones)
                        {
                            if (_opcdata.id < 0)
                            {
                                continue;
                            }
                            T_MENU_OPCION _menu_opc = new();
                            _menu_opc.Menu_c_iid = _menu.Menu_c_iid;
                            _menu_opc.Opc_c_iid = _opcdata.id;
                            _menu_opc.Menu_opcion_c_bestado = true;
                            _context_SS.MenuOpc.Add(_menu_opc);
                        }

                    }
                    _context_SS.SaveChanges();
                    //}
                    return Ok();
                }
                else
                {
                    var _menu = _context_SS.Menu.Where(e => e.Menu_c_iid.Equals(menu.id)).FirstOrDefault();
                    if (menu.parent_id != -1)
                    {
                        _menu.Menu_c_iid_padre = menu.parent_id;
                    }
                    else
                    {
                        _menu.Menu_c_iid_padre = null;
                    }
                    _menu.Menu_c_vnomb = menu.menu;
                    _menu.Menu_c_ynivel = menu.nivel;
                    _menu.Menu_c_vpag_asp = menu.pagina;
                    _menu.Menu_c_bestado = menu.estado;
                    _context_SS.Menu.Update(_menu);
                    _context_SS.SaveChanges();

                    var _menuopcs = _context_SS.MenuOpc.Where(e => e.Menu_c_iid.Equals(menu.id));
                    foreach (var _menuopc in _menuopcs)
                    {
                        var _menuprofile = _context_SS.Profile_menuopcion.Where(e => e.Menu_opcion_c_iid.Equals(_menuopc.Menu_opcion_c_iid)).FirstOrDefault();
                        if (!(_menuprofile == null))
                        {
                            _context_SS.Profile_menuopcion.Remove(_menuprofile);
                        }
                    }

                    _context_SS.SaveChanges();

                    foreach (var _menuopc in _menuopcs)
                    {

                        _context_SS.MenuOpc.Remove(_menuopc);
                    }
                    _context_SS.SaveChanges();
                    if (menu.opciones is not null)
                    {
                        foreach (NewOpcs _opcdata in menu.opciones)
                        {
                            if (_opcdata.id > 0)
                            {
                                T_MENU_OPCION _menu_opc = new();
                                _menu_opc.Menu_c_iid = menu.id;
                                _menu_opc.Opc_c_iid = _opcdata.id;
                                _menu_opc.Menu_opcion_c_bestado = true;
                                _context_SS.MenuOpc.Add(_menu_opc);
                            }
                        }
                    }
                    _context_SS.SaveChanges();

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
        public IActionResult Saveopc([FromBody] NewOPC OPC)
        {
            if (_engine.Equals("MSSQL"))
            {
                int intIdt = _context_SS.Opc.Max(u => u.Opc_c_iid);

                T_OPCION _opc = new();
                _opc.Opc_c_iid = intIdt + 1;
                _opc.Opc_c_vdesc = OPC.vdesc;
                _opc.Opc_c_bestado = OPC.estado;
                _context_SS.Opc.Add(_opc);
                _context_SS.SaveChanges();

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
        public IActionResult Deletemenu([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var childexist = _context_SS.Menu.Where(u => u.Menu_c_iid_padre.Equals(item.id)).Any();
                if (childexist)
                {
                    return Ok("2");
                }
                var exist = _context_SS.Profile_menu.Where(u => u.Menu_c_iid.Equals(item.id)).Any();
                if (exist)
                {
                    return Ok("1");
                }
                var _menu_option_items = _context_SS.MenuOpc.Where(u => u.Menu_c_iid.Equals(item.id));
                foreach (var _opcItem in _menu_option_items)
                {
                    _context_SS.MenuOpc.Remove(_opcItem);
                }
                _context_SS.SaveChanges();


                var _item = _context_SS.Menu.Where(u => u.Menu_c_iid.Equals(item.id)).FirstOrDefault();
                if (!(_item == null))
                {
                    _context_SS.Menu.Remove(_item);
                    _context_SS.SaveChanges();
                    return Ok();
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
        public IActionResult Opcs([FromBody] IdKey3 item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = (from A in _context_SS.Set<T_OPCION>()
                             join E in (
                                 from MO in _context_SS.Set<T_MENU_OPCION>()
                                 where MO.Menu_c_iid == item.id1
                                 select new
                                 {
                                     menu_c_iid = MO.Menu_c_iid,
                                     Opc_c_iid = MO.Opc_c_iid
                                 }
                             ) on A.Opc_c_iid equals E.Opc_c_iid into Detail
                             from M in Detail.DefaultIfEmpty()
                             select new
                             {
                                 opc_c_iid = A.Opc_c_iid,
                                 opc_c_vdesc = A.Opc_c_vdesc,
                                 checkedValue = M.menu_c_iid == null ? 0 : 1
                             }).ToList();

                //var queryA = from A in _context_SS.Set<T_PERFIL_MENU>()
                //             where A.Perf_c_yid == item.id2 && A.Menu_c_iid == item.id1 && A.Perf_menu_c_calta == 'A'
                //             select new
                //             {
                //                 opc_c_iid = -1,
                //                 opc_c_vdesc = "Nuevo",
                //                 checkedValue = 1
                //             };

                var queryA = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -1,
                                 opc_c_vdesc = "Nuevo",
                                 checkedValue = 1
                             };

                var queryB = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -2,
                                 opc_c_vdesc = "Editar",
                                 checkedValue = 1
                             };


                var queryC = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -3,
                                 opc_c_vdesc = "Ver",
                                 checkedValue = 1
                             };


                var queryD = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -4,
                                 opc_c_vdesc = "Procesar",
                                 checkedValue = 1
                             };


                var queryE = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -5,
                                 opc_c_vdesc = "Imprimir",
                                 checkedValue = 1
                             };


                var queryF = from A in _context_SS.Set<T_PERFIL_MENU>()
                             select new
                             {
                                 opc_c_iid = -6,
                                 opc_c_vdesc = "Eliminar",
                                 checkedValue = 1
                             };

                var fquery = queryA.Union(queryB).Union(queryC).Union(queryD).Union(queryE).Union(queryF);

                query.AddRange(fquery.ToList());

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
        public IActionResult getOpcsForView([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_SS.Set<T_MENU_OPCION>()
                            join E in _context_SS.Set<T_OPCION>() on A.Opc_c_iid equals E.Opc_c_iid into Detail
                            from M in Detail.DefaultIfEmpty()
                            select new
                            {
                                A.Menu_c_iid,
                                M.Opc_c_iid,
                                M.Opc_c_vdesc,
                                M.Opc_c_bestado
                            };

                query = query.Where(c => c.Menu_c_iid.Equals(item.id));

                return Ok(query);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

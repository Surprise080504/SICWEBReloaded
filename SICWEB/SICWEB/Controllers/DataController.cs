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
    public class DataController : ControllerBase
    {
        private readonly MainMssqlDbContext _context_MS;
        private readonly string _engine;
        public DataController(
            MainMssqlDbContext context_MS,
            IConfiguration configuration
        )
        {
            _context_MS = context_MS;
            _engine = configuration.GetConnectionString("ActiveEngine");

        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Menu([FromBody] IdKey2 item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_MS.Set<T_PERFIL_MENU>()
                            join E in _context_MS.Set<T_USUARIO_PERFIL>() on A.Perf_c_yid equals E.Perf_c_yid into F
                            from G in F.DefaultIfEmpty()
                            where G.Usua_c_cdoc_id == item.id
                            select new
                            {
                                A.Menu_c_iid
                            };


                List<int> lstMenuIds = new List<int>();
                foreach(var data in query)
                {
                    if (lstMenuIds.IndexOf(data.Menu_c_iid) == -1)
                    {
                        lstMenuIds.Add(data.Menu_c_iid);
                        List<int> pIDs = getAccessParentIDs(data.Menu_c_iid);
                        foreach (int pid in pIDs)
                        {
                            if (lstMenuIds.IndexOf(pid) == -1)
                            {
                                lstMenuIds.Add(pid);
                            }
                        }
                    }
                }

                List<MENU_PERMISION> _data = new List<MENU_PERMISION>();

                var permisionquery = from A in _context_MS.Set<T_MENU>()
                                     join B in _context_MS.Set<T_PERFIL_MENU>() on A.Menu_c_iid equals B.Menu_c_iid into C
                                     from D in C.DefaultIfEmpty()
                                     join E in _context_MS.Set<T_USUARIO_PERFIL>() on D.Perf_c_yid equals E.Perf_c_yid into F
                                     from G in F.DefaultIfEmpty()
                                     where G.Usua_c_cdoc_id == item.id
                                     select new
                                     {
                                         A.Menu_c_iid,
                                         A.Menu_c_iid_padre,
                                         A.Menu_c_vnomb,
                                         A.Menu_c_ynivel,
                                         A.Menu_c_vpag_asp,
                                         A.Menu_c_bestado,
                                         D.Perf_menu_c_calta,
                                         D.Perf_menu_c_cmod,
                                         D.Perf_menu_c_celim,
                                         D.Perf_menu_c_cvisual,
                                         D.Perf_menu_c_cimpre,
                                         D.Perf_menu_c_cproc,
                                         D.Perf_c_yid
                                     };


                List<int> lstForbiddenMenu = new List<int>();
                foreach (var _menu in permisionquery)
                {
                    if (lstMenuIds.IndexOf(_menu.Menu_c_iid) > -1)
                    {
                        MENU_PERMISION _menu_permision = new MENU_PERMISION();
                        _menu_permision.Menu_c_iid = _menu.Menu_c_iid;
                        _menu_permision.Menu_c_iid_padre = _menu.Menu_c_iid_padre;
                        _menu_permision.Menu_c_vnomb = _menu.Menu_c_vnomb;
                        _menu_permision.Menu_c_ynivel = _menu.Menu_c_ynivel;
                        _menu_permision.Menu_c_vpag_asp = _menu.Menu_c_vpag_asp;
                        _menu_permision.Menu_c_bestado = _menu.Menu_c_bestado;
                        _menu_permision.Perf_menu_c_calta = _menu.Perf_menu_c_calta;
                        _menu_permision.Perf_menu_c_cmod = _menu.Perf_menu_c_cmod;
                        _menu_permision.Perf_menu_c_celim = _menu.Perf_menu_c_celim;
                        _menu_permision.Perf_menu_c_cvisual = _menu.Perf_menu_c_cvisual;
                        _menu_permision.Perf_menu_c_cimpre = _menu.Perf_menu_c_cimpre;
                        _menu_permision.Perf_menu_c_cproc = _menu.Perf_menu_c_cproc;
                        _menu_permision.Perf_c_yid = _menu.Perf_c_yid;

                        if (_menu.Perf_menu_c_calta == 'B' && _menu.Perf_menu_c_cmod == 'B' && _menu.Perf_menu_c_celim == 'B'
                            && _menu.Perf_menu_c_cvisual == 'B' && _menu.Perf_menu_c_cimpre == 'B' && _menu.Perf_menu_c_cproc == 'B')
                            lstForbiddenMenu.Add(_menu.Menu_c_iid);
                            continue;

                        _data.Add(_menu_permision);
                    }
                }

                foreach (int pid in lstMenuIds)
                {
                    bool isExist = false;
                    foreach (MENU_PERMISION _menu_permision in _data)
                    {
                        if (_menu_permision.Menu_c_iid == pid)
                            isExist = true;
                    }

                    if (!isExist)
                    {
                        if (lstForbiddenMenu.Contains(pid))
                        {
                            continue;
                        }
                        MENU_PERMISION _menu_permision = new MENU_PERMISION();
                        _menu_permision.Menu_c_iid = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_iid;
                        _menu_permision.Menu_c_iid_padre = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_iid_padre;
                        _menu_permision.Menu_c_vnomb = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_vnomb;
                        _menu_permision.Menu_c_ynivel = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_ynivel;
                        _menu_permision.Menu_c_vpag_asp = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_vpag_asp;
                        _menu_permision.Menu_c_bestado = _context_MS.Menu.Where(u => u.Menu_c_iid.Equals(pid)).FirstOrDefault().Menu_c_bestado;
                        _menu_permision.Perf_menu_c_calta = 'A';
                        _menu_permision.Perf_menu_c_cmod = 'A';
                        _menu_permision.Perf_menu_c_celim = 'A';
                        _menu_permision.Perf_menu_c_cvisual = 'A';
                        _menu_permision.Perf_menu_c_cimpre = 'A';
                        _menu_permision.Perf_menu_c_cproc = 'A';

                        _data.Add(_menu_permision);
                    }
                }

                return Ok(_data);
            }
            else
            {
                return Ok();
            }
        }

        private List<int> getAccessParentIDs(int childID)
        {
            List<int> lstResult = new List<int>();

            foreach (var data in _context_MS.Menu)
            {
                if (data.Menu_c_iid == childID)
                {
                    if (data.Menu_c_iid_padre != null)
                    {
                        List<int> _data = getAccessParentIDs(data.Menu_c_iid_padre.Value);
                        lstResult.AddRange(_data);
                    }
                    else
                    {
                        lstResult.Add(data.Menu_c_iid);
                    }
                }
            }

            return lstResult;
        }
    }

    public class MENU_PERMISION
    {
        public int Menu_c_iid { get; set; }
        public int? Menu_c_iid_padre { get; set; }
        public string Menu_c_vnomb { get; set; }
        public byte? Menu_c_ynivel { get; set; }
        public string Menu_c_vpag_asp { get; set; }
        public bool Menu_c_bestado { get; set; }
        public Nullable<char> Perf_menu_c_calta { get; set; }
        public Nullable<char> Perf_menu_c_cmod { get; set; }
        public Nullable<char> Perf_menu_c_celim { get; set; }
        public Nullable<char> Perf_menu_c_cvisual { get; set; }
        public Nullable<char> Perf_menu_c_cimpre { get; set; }
        public Nullable<char> Perf_menu_c_cproc { get; set; }
        public int Perf_c_yid { get; set; }
    }
}

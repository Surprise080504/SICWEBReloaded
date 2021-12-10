
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
    public class UserController : ControllerBase
    {
        private readonly MainMssqlDbContext _context_SS;
        private readonly string _engine;
        [Obsolete]
        private IHostingEnvironment Environment;
        public UserController(
            MainMssqlDbContext context_SS,
            IConfiguration configuration,
            IHostingEnvironment _environment
        )
        {
            _context_SS = context_SS;
            _engine = configuration.GetConnectionString("ActiveEngine");
            Environment = _environment;

        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Users([FromBody] SearchUserKey searchKey)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_SS.Set<T_USUARIO>()
                            join P in _context_SS.Set<T_USUARIO_PERFIL>() on A.Usua_c_cdoc_id equals P.Usua_c_cdoc_id into D
                            from M in D.DefaultIfEmpty()
                            join PP in _context_SS.Set<T_PERFIL>() on M.Perf_c_yid equals PP.Perf_c_yid into F
                            from FF in F.DefaultIfEmpty()
                            select new
                            {
                                user = A.Usua_c_cdoc_id,
                                networkuser = A.Usua_c_cusu_red,
                                name = A.Usua_c_cape_nombres,
                                lastname = A.Usua_c_cape_pat,
                                mlastname = A.Usua_c_cape_mat,
                                profile = FF.Perf_c_vnomb,
                                profile_id = FF.Perf_c_yid,
                                password = A.Usua_c_vpass,
                                role = "",
                                roleprinciple = "",
                                estado = A.Usua_c_bestado == true ? 1 : 0
                            };

                if (!(searchKey.user == null) && !searchKey.user.Equals(""))
                {
                    query = query.Where(c => c.user.Contains(searchKey.user));

                }
                if (!(searchKey.networkuser == null) && !searchKey.networkuser.Equals(""))
                {
                    query = query.Where(c => c.networkuser.Contains(searchKey.networkuser));

                }
                if (!(searchKey.name == null) && !searchKey.name.Equals(""))
                {
                    query = query.Where(c => c.name.Contains(searchKey.name));

                }
                if (!(searchKey.surname == null) && !searchKey.surname.Equals(""))
                {
                    query = query.Where(c => (c.lastname + " " + c.mlastname).Contains(searchKey.surname));
                }

                if (!(searchKey.state == -1))
                {
                    query = query.Where(c => c.estado.Equals(searchKey.state));
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
        public IActionResult Getprofiles()
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_SS.Set<T_PERFIL>()
                            select new
                            {
                                A.Perf_c_yid,
                                A.Perf_c_vnomb
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
        public IActionResult Getprofile([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {

                var query = from A in _context_SS.Set<T_PERFIL>()
                            where A.Perf_c_yid == item.id
                            select new
                            {
                                id = A.Perf_c_yid,
                                profile = A.Perf_c_vnomb,
                                description = A.Perf_c_vdesc,
                                estado = A.Perf_c_cestado
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
        public IActionResult Saveuser([FromForm] NewUser userInfo)
        {
            if (_engine.Equals("MSSQL"))
            {
                string contentPath = this.Environment.ContentRootPath;
                IFormFile image = userInfo.userImage;
                if (image != null)
                {
                    string filePath = new FileUpload().Upload(image, contentPath, userInfo.user);
                }
                if (userInfo.method == -1)
                {
                    T_USUARIO _user = new();
                    _user.Usua_c_cape_nombres = userInfo.name;
                    _user.Usua_c_cape_pat = userInfo.lastname;
                    _user.Usua_c_cape_mat = userInfo.mlastname;
                    _user.Usua_c_cdoc_id = userInfo.user;
                    _user.Usua_c_vpass = userInfo.password;
                    _user.Usua_c_bestado = userInfo.estado;
                    _context_SS.User.Add(_user);
                    try
                    {
                        _context_SS.SaveChanges();

                        T_USUARIO_PERFIL _user_profile = new();
                        _user_profile.Perf_c_yid = userInfo.profile_id;
                        _user_profile.Usua_c_cdoc_id = userInfo.user;
                        _user_profile.Usua_perfil_c_cestado = '1';
                        _context_SS.UserProfile.Add(_user_profile);
                        _context_SS.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        return NotFound();
                    }


                    return Ok();
                }
                else
                {
                    var _user = _context_SS.User.Where(e => e.Usua_c_cdoc_id.Equals(userInfo.user)).FirstOrDefault();
                    _user.Usua_c_cape_nombres = userInfo.name;
                    _user.Usua_c_cape_pat = userInfo.lastname;
                    _user.Usua_c_cape_mat = userInfo.mlastname;
                    _user.Usua_c_vpass = userInfo.password;
                    _user.Usua_c_bestado = userInfo.estado;
                    _context_SS.User.Update(_user);
                    _context_SS.SaveChanges();

                    var _userProfile = _context_SS.UserProfile.Where(e => e.Usua_c_cdoc_id.Equals(userInfo.user)).FirstOrDefault();
                    if (_userProfile.Perf_c_yid != userInfo.profile_id)
                    {
                        _userProfile.Perf_c_yid = userInfo.profile_id;
                        _context_SS.UserProfile.Update(_userProfile);
                        _context_SS.SaveChanges();
                    }

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
        public IActionResult Saveprofile([FromBody] NewProfile profile)
        {
            if (_engine.Equals("MSSQL"))
            {
                if (profile.method == 0)
                {
                    //profile Insert
                    T_PERFIL _profile = new();
                    _profile.Perf_c_vnomb = profile.profile;
                    _profile.Perf_c_vdesc = profile.description;
                    if (profile.estado == 1)
                        _profile.Perf_c_cestado = '1';
                    else
                        _profile.Perf_c_cestado = '0';
                    _context_SS.Profile.Add(_profile);
                    _context_SS.SaveChanges();

                    //Profile update
                    List<string> lstParentIDs = new List<string>();
                    foreach (string value in profile.checkvalues)
                    {
                        if (!lstParentIDs.Contains(value.Split("-")[0]))
                        {
                            lstParentIDs.Add(value.Split("-")[0]);
                        }
                    }

                    var _curPerMenus = _context_SS.Profile_menu.Where(u => u.Perf_c_yid.Equals(_profile.Perf_c_yid));
                    foreach (var menu in _curPerMenus)
                    {
                        _context_SS.Profile_menu.Remove(menu);
                    }
                    _context_SS.SaveChanges();

                    //Seguridad.SIC_T_PERFIL_MENU
                    foreach (string parentID in lstParentIDs)
                    {
                        var _oldProfilemenu = _context_SS.Profile_menu.Where(u => u.Menu_c_iid.Equals(Int16.Parse(parentID))).Where(u => u.Perf_c_yid.Equals(_profile.Perf_c_yid)).FirstOrDefault();
                        if (!(_oldProfilemenu == null))
                        {
                            _oldProfilemenu.Perf_menu_c_calta = profile.checkvalues.Contains(parentID + "-" + "a") ? 'A' : 'B';
                            _oldProfilemenu.Perf_menu_c_cmod = profile.checkvalues.Contains(parentID + "-" + "b") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_celim = profile.checkvalues.Contains(parentID + "-" + "c") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cvisual = profile.checkvalues.Contains(parentID + "-" + "d") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cimpre = profile.checkvalues.Contains(parentID + "-" + "e") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cproc = profile.checkvalues.Contains(parentID + "-" + "f") ? 'A' : 'B'; ;
                            _context_SS.Profile_menu.Update(_oldProfilemenu);
                        }
                        else
                        {
                            T_PERFIL_MENU _menu = new();
                            _menu.Menu_c_iid = Int16.Parse(parentID);
                            _menu.Perf_c_yid = Convert.ToByte(_profile.Perf_c_yid);
                            _menu.Perf_menu_c_calta = profile.checkvalues.Contains(parentID + "-" + "a") ? 'A' : 'B';
                            _menu.Perf_menu_c_cmod = profile.checkvalues.Contains(parentID + "-" + "b") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_celim = profile.checkvalues.Contains(parentID + "-" + "c") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cvisual = profile.checkvalues.Contains(parentID + "-" + "d") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cimpre = profile.checkvalues.Contains(parentID + "-" + "e") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cproc = profile.checkvalues.Contains(parentID + "-" + "f") ? 'A' : 'B'; ;

                            _context_SS.Profile_menu.Add(_menu);
                        }


                    }
                    _context_SS.SaveChanges();

                    //Seguridad.SIC_T_PERFIL_MENU_OPCION
                    List<string> lstDefaultValues = new List<String>() { "a", "b", "c", "d", "e", "f" };
                    foreach (string value in profile.checkvalues)
                    {
                        if (lstDefaultValues.Contains(value.Split("-")[1]))
                            continue;

                        var _items = _context_SS.Profile_menuopcion.Where(u => u.Perf_c_yid.Equals(_profile.Perf_c_yid));
                        foreach (var _item in _items)
                        {
                            _context_SS.Profile_menuopcion.Remove(_item);
                        }

                        T_PERFIL_MENU_OPCION _menu_opcion = new();
                        _menu_opcion.Perf_c_yid = _profile.Perf_c_yid;
                        _menu_opcion.Menu_opcion_c_iid = Int16.Parse(value.Split("-")[1]);
                        _menu_opcion.Perf_menu_opcion_c_bestado = true;
                        _context_SS.Profile_menuopcion.Add(_menu_opcion);
                    }

                    _context_SS.SaveChanges();
                }
                else
                {
                    //profile update 
                    var _profile = _context_SS.Profile.Where(u => u.Perf_c_yid.Equals((byte)profile.id)).FirstOrDefault();
                    _profile.Perf_c_vnomb = profile.profile;
                    _profile.Perf_c_vdesc = profile.description;
                    if (profile.estado == 1)
                        _profile.Perf_c_cestado = '1';
                    else
                        _profile.Perf_c_cestado = '0';
                    _context_SS.Profile.Update(_profile);
                    _context_SS.SaveChanges();

                    //Profile update
                    List<string> lstParentIDs = new List<string>();
                    foreach (string value in profile.checkvalues)
                    {
                        if (!lstParentIDs.Contains(value.Split("-")[0]))
                        {
                            lstParentIDs.Add(value.Split("-")[0]);
                        }
                    }

                    var _curPerMenus = _context_SS.Profile_menu.Where(u => u.Perf_c_yid.Equals((byte)profile.id));
                    foreach (var menu in _curPerMenus)
                    {
                        _context_SS.Profile_menu.Remove(menu);
                    }
                    _context_SS.SaveChanges();

                    //Seguridad.SIC_T_PERFIL_MENU
                    foreach (string parentID in lstParentIDs)
                    {
                        var _oldProfilemenu = _context_SS.Profile_menu.Where(u => u.Menu_c_iid.Equals(Int16.Parse(parentID))).Where(u => u.Perf_c_yid.Equals((byte)profile.id)).FirstOrDefault();
                        if (!(_oldProfilemenu == null))
                        {
                            _oldProfilemenu.Perf_menu_c_calta = profile.checkvalues.Contains(parentID + "-" + "a") ? 'A' : 'B';
                            _oldProfilemenu.Perf_menu_c_cmod = profile.checkvalues.Contains(parentID + "-" + "b") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_celim = profile.checkvalues.Contains(parentID + "-" + "c") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cvisual = profile.checkvalues.Contains(parentID + "-" + "d") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cimpre = profile.checkvalues.Contains(parentID + "-" + "e") ? 'A' : 'B'; ;
                            _oldProfilemenu.Perf_menu_c_cproc = profile.checkvalues.Contains(parentID + "-" + "f") ? 'A' : 'B'; ;
                            _context_SS.Profile_menu.Update(_oldProfilemenu);
                        }
                        else
                        {
                            T_PERFIL_MENU _menu = new();
                            _menu.Menu_c_iid = Int16.Parse(parentID);
                            _menu.Perf_c_yid = Convert.ToByte(profile.id);
                            _menu.Perf_menu_c_calta = profile.checkvalues.Contains(parentID + "-" + "a") ? 'A' : 'B';
                            _menu.Perf_menu_c_cmod = profile.checkvalues.Contains(parentID + "-" + "b") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_celim = profile.checkvalues.Contains(parentID + "-" + "c") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cvisual = profile.checkvalues.Contains(parentID + "-" + "d") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cimpre = profile.checkvalues.Contains(parentID + "-" + "e") ? 'A' : 'B'; ;
                            _menu.Perf_menu_c_cproc = profile.checkvalues.Contains(parentID + "-" + "f") ? 'A' : 'B'; ;

                            _context_SS.Profile_menu.Add(_menu);
                        }


                    }
                    _context_SS.SaveChanges();

                    //Seguridad.SIC_T_PERFIL_MENU_OPCION
                    List<string> lstDefaultValues = new List<String>() { "a", "b", "c", "d", "e", "f" };
                    foreach (string value in profile.checkvalues)
                    {

                        var _items = _context_SS.Profile_menuopcion.Where(u => u.Perf_c_yid.Equals((byte)profile.id));
                        foreach (var _item in _items)
                        {
                            _context_SS.Profile_menuopcion.Remove(_item);
                        }

                        if (value.Split("-").Length < 2)
                        {
                            continue;
                        }
                        if (lstDefaultValues.Contains(value.Split("-")[1]))
                            continue;


                        T_PERFIL_MENU_OPCION _menu_opcion = new();
                        _menu_opcion.Perf_c_yid = (byte)profile.id;
                        _menu_opcion.Menu_opcion_c_iid = Int16.Parse(value.Split("-")[1]);
                        _menu_opcion.Perf_menu_opcion_c_bestado = true;
                        _context_SS.Profile_menuopcion.Add(_menu_opcion);
                    }

                    _context_SS.SaveChanges();

                    return Ok();
                }
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
        public IActionResult Deleteuser([FromBody] IdKey2 item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var _useritems = _context_SS.UserProfile.Where(u => u.Usua_c_cdoc_id.Equals(item.id));
                foreach (var _useritem in _useritems)
                {
                    _context_SS.UserProfile.Remove(_useritem);
                }
                _context_SS.SaveChanges();

                var _user = _context_SS.User.Where(u => u.Usua_c_cdoc_id.Equals(item.id)).FirstOrDefault();
                if (!(_user == null))
                {
                    _context_SS.User.Remove(_user);
                    _context_SS.SaveChanges();
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
        public IActionResult Checkimagefile([FromBody] IdKey2 item)
        {
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
        public IActionResult Getaccessprofile(int profileid)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from A in _context_SS.Set<T_MENU>()
                            join B in _context_SS.Set<T_MENU_OPCION>() on A.Menu_c_iid equals B.Menu_c_iid into C
                            from D in C.DefaultIfEmpty()
                            join E in _context_SS.Set<T_OPCION>() on D.Opc_c_iid equals E.Opc_c_iid into F
                            from G in F.DefaultIfEmpty()
                            select new
                            {
                                value = A.Menu_c_iid,
                                childvalue = D.Menu_opcion_c_iid == null ? 0 : D.Menu_opcion_c_iid,
                                label = A.Menu_c_vnomb,
                                childlabel = G.Opc_c_vdesc == null ? "" : G.Opc_c_vdesc,
                                parentid = A.Menu_c_iid_padre == null ? 0 : A.Menu_c_iid_padre
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
        public IActionResult Getcheckedvalues([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from B in _context_SS.Set<T_MENU_OPCION>()
                            join E in _context_SS.Set<T_PERFIL_MENU_OPCION>() on B.Menu_opcion_c_iid equals E.Menu_opcion_c_iid into F
                            from G in F.DefaultIfEmpty()
                            where G.Perf_c_yid == item.id
                            select new
                            {
                                menuid = B.Menu_c_iid,
                                menuopcionid = G.Menu_opcion_c_iid
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
        public IActionResult Getcheckedcrudvalues([FromBody] IdKey item)
        {
            if (_engine.Equals("MSSQL"))
            {
                var query = from B in _context_SS.Set<T_PERFIL_MENU>()
                            where B.Perf_c_yid == (byte)item.id
                            select new
                            {
                                menuid = B.Menu_c_iid,
                                a = B.Perf_menu_c_calta,
                                b = B.Perf_menu_c_cmod,
                                c = B.Perf_menu_c_celim,
                                d = B.Perf_menu_c_cvisual,
                                e = B.Perf_menu_c_cimpre,
                                f = B.Perf_menu_c_cproc,
                            };

                return Ok(query);
            }
            else
            {
                return NotFound();
            }
        }

    }

    public class FileUpload
    {
        public string Upload(IFormFile file, string contentPath, string userid)
        {
            var uploadDirectory = "ClientApp/public/uploads/";
            var uploadPath = Path.Combine(contentPath, uploadDirectory);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            //var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadPath, "estilo_image_" + userid + ".png");

            using (var strem = File.Create(filePath))
            {
                file.CopyTo(strem);
            }
            return filePath;
        }
    }
}

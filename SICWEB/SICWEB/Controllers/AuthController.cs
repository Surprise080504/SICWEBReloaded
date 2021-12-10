using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SICWEB.DbFactory;
// using SICWEB.Hubs;
using SICWEB.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SICWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        // private readonly IHubContext<UsersHub> _hubContext;
        private readonly MainMssqlDbContext _context_MS;
        private readonly IConfiguration _configuration;
        private readonly string _engine;
        public AuthController(
            // IHubContext<UsersHub> usersHub,
            MainMssqlDbContext context_MS,
            IConfiguration configuration
        )
        {
            // _hubContext = usersHub;
            _context_MS = context_MS;
            _configuration = configuration;
            _engine = configuration.GetConnectionString("ActiveEngine");

        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody] Credentials request)
        {

#if DEBUG
            request.Email = "ADMIN";
            request.Password = "juan899833245";

#endif
            var authUser = new AuthUser("fail", "", request.UserName ?? request.Email, request.Email);
            if (_engine.Equals("MSSQL"))
            {
                var loginQuery = _context_MS.USUARIO.Where(u => u.Usua_c_cdoc_id.Equals(request.UserName ?? request.Email) && u.Usua_c_vpass.Equals(request.Password)).Any();
                if (!loginQuery)
                    return Ok(new AuthUser("fail", "El usuario y/o contraseña, son incorrectos.", "", ""));
                else
                {
                    var estadoQuery = _context_MS.USUARIO.Where(u => u.Usua_c_cdoc_id.Equals(request.UserName ?? request.Email) && u.Usua_c_vpass.Equals(request.Password) && u.Usua_c_bestado.Equals(true)).Any();
                    if (!estadoQuery)
                    {
                        return Ok(new AuthUser("inactive", "El usuario actual está inactivo.", "", ""));
                    }
                    else
                    {
                        authUser.Status = "success";
                        authUser.Token = CreateToken(authUser);
                    }
                }
            }
            else
            {
                authUser.Token = CreateToken(authUser);
            }
            // await _hubContext.Clients.All.SendAsync("UserLogin");
            return Ok(authUser);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Credentials request)
        {
            var authUser = new AuthUser("success", "", request.UserName,  request.Email);
            if (_engine.Equals("MSSQL"))
            {
                if (_context_MS.USUARIO.Where(u => u.Usua_c_cdoc_id.Equals(request.UserName)).Any())
                    return Ok(new AuthUser("fail", "El nombre de usuario ya existe.", "", ""));
                else
                {
                    try
                    {
                        T_USUARIO user = new()
                        {
                            Usua_c_cusu_red = "",
                            Usua_c_cdoc_id = authUser.UserName,
                            Usua_c_vpass = request.Password,
                            Usua_c_bestado = true
                        };
                        await _context_MS.USUARIO.AddAsync(user);
                        await _context_MS.SaveChangesAsync();
                    }
                    catch (Exception) { }
                    authUser.Token = CreateToken(authUser);
                }
            }
            else
            {
                authUser.Token = CreateToken(authUser);
            }
            // await _hubContext.Clients.All.SendAsync("UserLogin");
            return Ok(authUser);
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        [ProducesResponseType(typeof(AuthUser), StatusCodes.Status200OK)]
        public IActionResult Me()
        {

            return Ok();
        }


        public string CreateToken(AuthUser user)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetConnectionString("AppSecret"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, "cliente")
                }),
                Expires = DateTime.UtcNow.AddHours(1.0),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
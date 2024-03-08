using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using PRN231.ASS01.BookStoreAPI.Models;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;
using PRN231.ASS01.BookStoreAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace PRN231.ASS01.BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        //private readonly IAccountRepo _accountRepo;
        //private readonly IConfiguration _config;
        //public LoginController(IAccountRepo accountRepo, IConfiguration config)
        //{
        //    _accountRepo = accountRepo;
        //    _config = config;
        //}
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Role> _roleRepo;
        private readonly IConfiguration _config;
        public AuthController(IRepository<User> userRepo, IRepository<Role> roleRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //var user = await _userRepo.Get(request.Email, request.Password);
            User user = await _userRepo.FindByIdAsync(u => u.EmailAddress == request.Email && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.Role, user.RoleId.ToString()),

    };
            var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return Ok(new LoginResponse
            {
                Token = token,
            });
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh()
        {
            return Ok();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok();
        }
    }
}

using API.Models;
using API.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AuthService _authService;

        public AuthController(IConfiguration configuration, AuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            //pass check
            User? user = _authService.checkPassword(username,password);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)), // время действия 2 дня
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return StatusCode(200, new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        [HttpPost("Register")]
        public IActionResult Register(string username, string password)
        {
            //pass check
            UserAuthData authData = new UserAuthData()
            {
                UserName = username,
                Password = password,
                User = new User()
                {
                    Name = username
                }
            };

            _authService.CreateUser(authData);

            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

            //var jwt = new JwtSecurityToken(
            //        issuer: AuthOptions.ISSUER,
            //        audience: AuthOptions.AUDIENCE,
            //        claims: claims,
            //        expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)), // время действия 2 дня
            //        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            //return StatusCode(200, new JwtSecurityTokenHandler().WriteToken(jwt));
            return StatusCode(200);
        }

        [HttpGet("ValidateToken")]
        public bool ValidateToken(string token)
        {
            var securityKey = AuthOptions.GetSymmetricSecurityKey();
            try
            {
                JwtSecurityTokenHandler handler = new();
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    //ValidAudience = "" //if ValidateAudience = true
                    ValidateIssuer = false,
                    //ValidIssuer = "" //if ValidateIssuer = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var claims = jwtToken.Claims.ToList(); //to get claims
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}

using Backend.BusinessLogic;
using Backend.BusinessLogic.Account;
using Backend.Common.DTOs;
using Backend.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccountController : BaseController
    {
        private readonly UserAccountService _service;
        private readonly IConfiguration _configuration;
        public UserAccountController(ControllerDependencies dependencies, UserAccountService service, IConfiguration configuration) : base(dependencies)
        {
            _service = service;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            _service.RegisterNewUser(model);

            var user = await _service.Login(new LoginModel()
            {
                Password = model.PasswordString,
                Email = model.Email,
                AreCredentialsInvalid = false,
                IsDisabled = false
            });

            var token = LogIn(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                username = user.Username
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _service.Login(model);

            if (!user.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return Unauthorized();
            }

            var token = LogIn(user);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                username = user.Username
            });

        }

        private JwtSecurityToken LogIn(CurrentUserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                new Claim("UserName", $"{user.Username}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("IsDisabled", $"{user.IsDisabled}")
            };

            user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var token = GetToken(claims);

            return token;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var y = _configuration["JWT:Secret"];
            var x = Encoding.UTF8.GetBytes(y);
            var authSigningKey = new SymmetricSecurityKey(x);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

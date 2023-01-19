using Backend.BusinessLogic;
using Backend.BusinessLogic.Account;
using Backend.Common.DTOs;
using Backend.WebApp.Code.Base;
using Backend.WebApp.Code.Utils;
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
                username = user.Username,
                Roles = user.Roles
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
                username = user.Username,
                Roles = user.Roles
            });
        }

        [HttpGet("profilePage")]
        [Authorize]
        public IActionResult ProfilePage()
        {
            var model = _service.GetUserInfo(CurrentUser.Id);
            return Ok(model);
        }

        [HttpGet("getEditProfileModel")]
        [Authorize]
        public IActionResult EditProfile()
        {
            var model = _service.GetEditModel(CurrentUser.Id);
            return Ok(model);
        }

        [HttpPost("editProfile")]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileModel model)
        {
             _service.EditProfile(model, CurrentUser.Id);
            /*if (newUsername != CurrentUser.Username)
            {
                var user = CurrentUser;
                await utils.LogOut(HttpContext);
                user.Username = newUsername;
                await utils.LogIn(user, HttpContext);
            }*/
            return Ok();
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
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(4),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha512Signature)
                );

            return token;
        }
    }
}

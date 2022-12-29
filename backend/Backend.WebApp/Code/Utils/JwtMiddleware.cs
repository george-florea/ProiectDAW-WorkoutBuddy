using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Backend.BusinessLogic.Account;
using System.Security.Claims;

namespace Backend.WebApp.Code.Utils
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration1)
        {
            _next = next;
            _configuration = configuration1;
        }

        public async Task Invoke(HttpContext context, UserAccountService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var newContext = attachUserToContext(context, userService, token);
                await _next(newContext);
            }
            else
            {
                await _next(context);
            }
        }

        private HttpContext attachUserToContext(HttpContext context, UserAccountService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claims = jwtToken.Claims;
                
                var claimIdentity = new ClaimsIdentity(claims);

                context.User = new ClaimsPrincipal(claimIdentity);
            }
            catch
            {
            }
            return context;
        }
    }
}

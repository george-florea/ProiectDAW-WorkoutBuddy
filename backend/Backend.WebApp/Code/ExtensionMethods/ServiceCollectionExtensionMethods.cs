using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Backend.Common.DTOs;
using System;
using System.Linq;
using Backend.WebApp.Code.Base;
using Backend.BusinessLogic;
using Backend.BusinessLogic.Base;
using System.Security.Claims;
using Backend.BusinessLogic.Account;

namespace Backend.WebApp.Code.ExtensionMethods
{
    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddBackendBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<UserAccountService>();
            return services;
        }

        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);
                var usernameClaim = claims?.FirstOrDefault(c => c.Type == "UserName")?.Value;
                var isDisabledClaim = claims?.FirstOrDefault(c => c.Type == "IsDisabled")?.Value;
                var nameClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var emailClaim = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var rolesClaim = claims?.Where(c => c.Type == ClaimTypes.Role)?.Select(c => c.Value).ToList();

                //var activeRoleId = claims?.FirstOrDefault(c => c.Type == "ActiveRoleId")?.Value;

                //var newClaims = new List<Claim>
                //{
                //    new Claim("ActiveRoleId", "3")
                //};

                //var appIdentity = new ClaimsIdentity(newClaims);
                //httpContext.User.AddIdentity(appIdentity);

                return new CurrentUserDto
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Name = nameClaim,
                    Username = usernameClaim,
                    Email = emailClaim,
                    Roles = rolesClaim,
                    IsDisabled = isDisabledClaim == "True"
                };
            });

            return services;
        }
    }
}


using Backend.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Backend.WebApp.Code.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        readonly string _claim;

        public AuthorizeAttribute(string claim = "")
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Claims.Count() == 0)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            if(_claim == "admin" &&  !user.Claims.Select(c => c.Value).ToList().Contains("Admin"))
            {
                // user is not admin
                context.Result = new JsonResult(new { message = "Unauthorized - Not Admin" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

    }
}

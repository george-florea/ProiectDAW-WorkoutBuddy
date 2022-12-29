
using Backend.Common.DTOs;
using Backend.WebApp.Code.ExtensionMethods;

namespace Backend.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDto CurrentUser { get; set; }

        public ControllerDependencies(HttpContext context)
        {
            CurrentUser = context.AddCurrentUser();
        }
    }
}

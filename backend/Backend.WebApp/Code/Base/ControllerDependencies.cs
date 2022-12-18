
using Backend.Common.DTOs;

namespace Backend.WebApp.Code.Base
{
    public class ControllerDependencies
    {
        public CurrentUserDto CurrentUser { get; set; }

        public ControllerDependencies(CurrentUserDto currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}

using System;

namespace Backend.BusinessLogic.Account
{
    public class RegisterModel
    {
        public string? Email { get; set; }
        public string? PasswordString { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public DateTime? BirthDay { get; set; }
        public double? Weight { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Backend.Common.DTOs
{
    public class CurrentUserDto
    {
        public CurrentUserDto()
        {
            Roles = new List<string>();
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsDisabled { get; set; }
        public List<string> Roles { get; set; }

    }
}

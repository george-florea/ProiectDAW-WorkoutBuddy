using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Account
{
    public class EditUserModel
    {
        public EditUserModel()
        {
            Roles = new List<int>();
        }

        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<int>? Roles { get; set; }
    }
}

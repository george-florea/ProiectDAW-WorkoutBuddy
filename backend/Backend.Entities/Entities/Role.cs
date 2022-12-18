using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Role
    {
        public Role()
        {
            Idpermissons = new HashSet<Permission>();
            Idusers = new HashSet<User>();
        }

        public int Idrole { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Permission> Idpermissons { get; set; }
        public virtual ICollection<User> Idusers { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Split
    {
        public Split()
        {
            Comments = new HashSet<Comment>();
            UserSplits = new HashSet<UserSplit>();
            Workouts = new HashSet<Workout>();
        }

        public Guid Idsplit { get; set; }
        public string Name { get; set; } = null!;
        public Guid Idcreator { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; }

        public virtual User IdcreatorNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<UserSplit> UserSplits { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}

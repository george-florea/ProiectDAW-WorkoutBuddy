using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserSplit
    {
        public UserSplit()
        {
            UserWorkouts = new HashSet<UserWorkout>();
        }

        public Guid Iduser { get; set; }
        public Guid Idsplit { get; set; }
        public int? Rating { get; set; }

        public virtual Split IdsplitNavigation { get; set; } = null!;
        public virtual User IduserNavigation { get; set; } = null!;
        public virtual ICollection<UserWorkout> UserWorkouts { get; set; }
    }
}

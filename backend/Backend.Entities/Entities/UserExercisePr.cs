using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserExercisePr
    {
        public Guid Iduser { get; set; }
        public Guid Idexercise { get; set; }
        public Guid Idpr { get; set; }
        public decimal? OneRepMax { get; set; }

        public virtual Exercise IdexerciseNavigation { get; set; } = null!;
        public virtual User IduserNavigation { get; set; } = null!;
    }
}

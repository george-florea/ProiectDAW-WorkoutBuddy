using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class WorkoutExercise
    {
        public WorkoutExercise()
        {
            UserExercises = new HashSet<UserExercise>();
        }

        public Guid Idworkout { get; set; }
        public Guid Idexercise { get; set; }

        public virtual Exercise IdexerciseNavigation { get; set; } = null!;
        public virtual Workout IdworkoutNavigation { get; set; } = null!;
        public virtual ICollection<UserExercise> UserExercises { get; set; }
    }
}

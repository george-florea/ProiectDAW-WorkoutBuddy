using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserWorkout
    {
        public UserWorkout()
        {
            UserExercises = new HashSet<UserExercise>();
        }

        public Guid Iduser { get; set; }
        public Guid Idsplit { get; set; }
        public Guid Idworkout { get; set; }
        public DateTime Date { get; set; }
        public decimal? WorkoutEffortCoefficient { get; set; }

        public virtual UserSplit Id { get; set; } = null!;
        public virtual Workout IdNavigation { get; set; } = null!;
        public virtual ICollection<UserExercise> UserExercises { get; set; }
    }
}

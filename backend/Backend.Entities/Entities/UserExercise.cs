using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserExercise
    {
        public UserExercise()
        {
            UserExerciseSets = new HashSet<UserExerciseSet>();
        }

        public Guid Iduser { get; set; }
        public Guid Idworkout { get; set; }
        public Guid Idexercise { get; set; }
        public DateTime Date { get; set; }
        public int? SetsNo { get; set; }
        public decimal? EffortCoefficient { get; set; }

        public virtual WorkoutExercise Id { get; set; } = null!;
        public virtual UserWorkout UserWorkout { get; set; } = null!;
        public virtual ICollection<UserExerciseSet> UserExerciseSets { get; set; }
    }
}

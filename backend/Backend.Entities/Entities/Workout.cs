using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Workout
    {
        public Workout()
        {
            UserWorkouts = new HashSet<UserWorkout>();
            WorkoutExercises = new HashSet<WorkoutExercise>();
        }

        public Guid Idworkout { get; set; }
        public Guid Idsplit { get; set; }
        public string Name { get; set; } = null!;

        public virtual Split IdsplitNavigation { get; set; } = null!;
        public virtual ICollection<UserWorkout> UserWorkouts { get; set; }
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}

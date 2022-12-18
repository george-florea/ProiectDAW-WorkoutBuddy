using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Exercise
    {
        public Exercise()
        {
            UserExercisePrs = new HashSet<UserExercisePr>();
            WorkoutExercises = new HashSet<WorkoutExercise>();
            Idgroups = new HashSet<MuscleGroup>();
        }

        public Guid Idexercise { get; set; }
        public int Idtype { get; set; }
        public Guid? Idimage { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsPending { get; set; }

        public virtual Image? IdimageNavigation { get; set; }
        public virtual ExerciseType IdtypeNavigation { get; set; } = null!;
        public virtual ICollection<UserExercisePr> UserExercisePrs { get; set; }
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }

        public virtual ICollection<MuscleGroup> Idgroups { get; set; }
    }
}

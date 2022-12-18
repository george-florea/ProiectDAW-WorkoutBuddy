using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class ExerciseType
    {
        public ExerciseType()
        {
            Exercises = new HashSet<Exercise>();
        }

        public int Idtype { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}

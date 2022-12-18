using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserExerciseSet
    {
        public Guid Iduser { get; set; }
        public Guid Idworkout { get; set; }
        public Guid Idexercise { get; set; }
        public DateTime Date { get; set; }
        public Guid Idset { get; set; }
        public int? Duration { get; set; }
        public double? Distance { get; set; }
        public int? RepsNo { get; set; }
        public double? Weight { get; set; }

        public virtual UserExercise UserExercise { get; set; } = null!;
    }
}

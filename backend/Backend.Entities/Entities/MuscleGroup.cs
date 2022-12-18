using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class MuscleGroup
    {
        public MuscleGroup()
        {
            Idexercises = new HashSet<Exercise>();
        }

        public int Idgroup { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Exercise> Idexercises { get; set; }
    }
}

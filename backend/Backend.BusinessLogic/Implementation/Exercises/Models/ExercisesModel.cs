using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Exercises
{
    public class ExercisesModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? IdImage { get; set; }
        public string ExerciseType { get; set; }
        public List<string?> MuscleGroups { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Common.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Backend.BusinessLogic.Exercises
{
    public class InsertExerciseModel
    {
        public InsertExerciseModel()
        {
            ExerciseTypes = new List<ListItemModel<string, int>>();
            MuscleGroups = new List<ListItemModel<string, int>>();
            SelectedMuscleGroups = new List<ListItemModel<string, int>>();
        }

        public Guid ExerciseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ListItemModel<string, int>>? ExerciseTypes { get; set; }
        public ListItemModel<string, int>? SelectedType { get; set; }
        public List<ListItemModel<string, int>>? MuscleGroups { get; set; }
        public List<ListItemModel<string, int>>? SelectedMuscleGroups { get; set; }
        public IFormFile? Image { get; set; }
    }
}

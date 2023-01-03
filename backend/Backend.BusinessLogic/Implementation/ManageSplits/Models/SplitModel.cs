using Backend.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Splits
{
    public class SplitModel
    {
        public Guid SplitId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<ListItemModel<string, int>>? MusclesGroups { get; set; }
        public Guid CreatorId { get; set; }
        public List<WorkoutModel>? Workouts { get; set; }
        public bool? IsPrivate { get; set; }
    }
}

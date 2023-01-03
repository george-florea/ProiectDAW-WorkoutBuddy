using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Backend.BusinessLogic.Splits
{
    public class ViewSplitModel
    {
        public Guid SplitId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string CreatorName { get; set; }
        public Guid CreatorId { get; set; }
        public float Rating { get; set; }
        public List<ListWorkoutModel> Workouts { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}

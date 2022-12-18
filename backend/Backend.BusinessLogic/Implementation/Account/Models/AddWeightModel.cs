using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Account
{
    public class AddWeightModel
    {
        public Guid UserId { get; set; }
        public float Weight { get; set; }
        public List<WeightHistoryModel> History { get; set; }
    }
}

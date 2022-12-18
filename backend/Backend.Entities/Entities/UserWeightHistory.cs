using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserWeightHistory
    {
        public Guid Iduser { get; set; }
        public DateTime WeighingDate { get; set; }
        public double Weight { get; set; }

        public virtual User IduserNavigation { get; set; } = null!;
    }
}

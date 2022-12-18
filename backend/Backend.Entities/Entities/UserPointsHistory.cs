using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class UserPointsHistory
    {
        public Guid Iduser { get; set; }
        public DateTime ObtainDate { get; set; }
        public int PointsNo { get; set; }
        public int? Reasonid { get; set; }

        public virtual User IduserNavigation { get; set; } = null!;
        public virtual Reason? Reason { get; set; }
    }
}

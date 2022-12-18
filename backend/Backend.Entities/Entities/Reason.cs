using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Reason
    {
        public Reason()
        {
            UserPointsHistories = new HashSet<UserPointsHistory>();
        }

        public int ReasonId { get; set; }
        public string? Reason1 { get; set; }

        public virtual ICollection<UserPointsHistory> UserPointsHistories { get; set; }
    }
}

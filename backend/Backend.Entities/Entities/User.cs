using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class User
    {
        public User()
        {
            Splits = new HashSet<Split>();
            UserExercisePrs = new HashSet<UserExercisePr>();
            UserPointsHistories = new HashSet<UserPointsHistory>();
            UserSplits = new HashSet<UserSplit>();
            UserWeightHistories = new HashSet<UserWeightHistory>();
            Idroles = new HashSet<Role>();
        }

        public Guid Iduser { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public string Username { get; set; } = null!;
        public Guid Salt { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastModifiedOn { get; set; }

        public virtual ICollection<Split> Splits { get; set; }
        public virtual ICollection<UserExercisePr> UserExercisePrs { get; set; }
        public virtual ICollection<UserPointsHistory> UserPointsHistories { get; set; }
        public virtual ICollection<UserSplit> UserSplits { get; set; }
        public virtual ICollection<UserWeightHistory> UserWeightHistories { get; set; }

        public virtual ICollection<Role> Idroles { get; set; }
    }
}

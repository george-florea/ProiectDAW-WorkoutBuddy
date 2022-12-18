using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Comment
    {
        public Comment()
        {
            InverseIdparentCommNavigation = new HashSet<Comment>();
        }

        public Guid Idcomment { get; set; }
        public Guid Idsplit { get; set; }
        public string CommentText { get; set; } = null!;
        public Guid? IdparentComm { get; set; }
        public Guid Iduser { get; set; }

        public virtual Comment? IdparentCommNavigation { get; set; }
        public virtual Split IdsplitNavigation { get; set; } = null!;
        public virtual ICollection<Comment> InverseIdparentCommNavigation { get; set; }
    }
}

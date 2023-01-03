using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Splits
{
    
    public class CommentModel
    {
        public Guid CommentId { get; set; }
        public string CommentText { get; set; }
        public List<CommentModel> CommentReplys { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string Author { get; set; }
        public string AuthorRole { get; set; }
        public Guid ParentSplitID { get; set; }
    }
}

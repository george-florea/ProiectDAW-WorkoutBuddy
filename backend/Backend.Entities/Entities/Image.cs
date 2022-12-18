using System;
using System.Collections.Generic;

namespace Backend.Entities
{
    public partial class Image
    {
        public Image()
        {
            Exercises = new HashSet<Exercise>();
        }

        public Guid Idimg { get; set; }
        public byte[] ImgContent { get; set; } = null!;

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}

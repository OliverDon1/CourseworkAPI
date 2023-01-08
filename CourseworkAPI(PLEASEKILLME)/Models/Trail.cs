using System;
using System.Collections.Generic;

namespace CourseworkAPI_PLEASEKILLME_.Models
{
    public partial class Trail
    {
        public Trail()
        {
            Archives = new HashSet<Archive>();
            Comments = new HashSet<Comment>();
        }

        public int TrailId { get; set; }
        public string TrailName { get; set; } = null!;
        public int TrailLength { get; set; }
        public int TrailRating { get; set; }

        public virtual ICollection<Archive> Archives { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

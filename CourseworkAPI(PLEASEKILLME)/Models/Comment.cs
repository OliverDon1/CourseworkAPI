using System;
using System.Collections.Generic;

namespace CourseworkAPI_PLEASEKILLME_.Models
{
    public partial class Comment
    {

        public int CommentId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime? CommentDate { get; set; }
        public int? TrailId { get; set; }
        public int? UserId { get; set; }
    }
}

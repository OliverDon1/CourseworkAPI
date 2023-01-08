using System;
using System.Collections.Generic;

namespace CourseworkAPI_PLEASEKILLME_.Models
{
    public partial class Archive
    {
        public int ArchiveId { get; set; }
        public string CommentText { get; set; } = null!;
        public DateTime CommentDate { get; set; }
        public DateTime ArchiveDate { get; set; }
        public int? UserId { get; set; }
        public int? TrailId { get; set; }
        public int? CommentId { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Trail? Trail { get; set; }
        public virtual User? User { get; set; }
    }
}

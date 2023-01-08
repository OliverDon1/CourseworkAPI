using System;
using System.Collections.Generic;

namespace CourseworkAPI_PLEASEKILLME_.Models
{
    public partial class User
    {
        public User()
        {
            Archives = new HashSet<Archive>();
            Comments = new HashSet<Comment>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public DateTime SignupDate { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Archive> Archives { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

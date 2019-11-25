using System;
using System.Collections.Generic;

namespace Capstone4.Models
{
    public partial class Tasks
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDesc { get; set; }
        public DateTime TaskDueDate { get; set; }
        public bool? TaskIsComplete { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}

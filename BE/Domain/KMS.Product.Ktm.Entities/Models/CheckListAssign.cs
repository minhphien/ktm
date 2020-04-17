using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class CheckListAssign : BaseEntity
    {
        public int AssigneeId { get; set; }

        public string AssigneeComment { get; set; }

        public string MentorComment { get; set; }

        public int StatusId { get; set; }

        public virtual CheckListStatus Status { get; set; }

        public virtual Employee Assignee { get; set; }
    }
}

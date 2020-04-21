using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class AssignmentItem : BaseEntity
    {
        public string AssigneeComment { get; set; }

        public string MentorComment { get; set; }

        public int CheckListItemId { get; set; }

        public int StatusId { get; set; }

        public int AssignmentId { get; set; }

        public virtual Status Status { get; set; }

        public virtual Assignment Assignment { get; set; }

        public virtual CheckListItem CheckListItem { get; set; }
    }
}

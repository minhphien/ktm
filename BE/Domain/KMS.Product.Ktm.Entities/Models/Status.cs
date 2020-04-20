using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class Status : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<AssignmentItem> AssignmentItems { get; set; }
    }
}

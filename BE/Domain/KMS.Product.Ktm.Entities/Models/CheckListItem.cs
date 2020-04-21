using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class CheckListItem : BaseEntity
    {
        public int ItemId { get; set; }

        public int CheckListId { get; set; }

        public virtual Item Item { get; set; }

        public virtual CheckList CheckList { get; set; }

        public virtual IEnumerable<AssignmentItem> AssignmentItems { get; set; }
    }
}

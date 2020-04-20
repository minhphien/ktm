using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class CheckList : BaseEntity
    {
        public string Detail { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual IEnumerable<CheckListItem> CheckListItems { get; set; }
    }
}

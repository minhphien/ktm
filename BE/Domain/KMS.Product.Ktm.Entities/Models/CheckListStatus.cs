using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class CheckListStatus : BaseEntity
    {
        public string Status { get; set; }

        public virtual ICollection<CheckListAssign> CheckListAssigns { get; set; }
    }
}

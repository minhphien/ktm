using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class CheckListItem : BaseEntity
    {
        public string Tilte { get; set; }

        public string Detail { get; set; }

        public int CreatorId { get; set; }

        public int TeamId { get; set; }

        public virtual Employee Creator { get; set; }

        public virtual Team Team { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Entities.Models
{
    public class KudoDetail : BaseEntity
    {
        public string Content { get; set; }
        public int KudoTypeId { get; set; }

        public virtual ICollection<Kudo> Kudos { get; set; }
        public virtual KudoType KudoType { get; set; }

    }
}

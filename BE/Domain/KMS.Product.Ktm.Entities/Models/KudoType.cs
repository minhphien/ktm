using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMS.Product.Ktm.Entities.Models
{
    public class KudoType : BaseEntity
    {
        public string TypeName { get; set; }

        public virtual ICollection<KudoDetail> KudoDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMS.Product.Ktm.Repository.Models
{
    public class KudoType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KudoTypeID { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Kudo> Kudos { get; set; }
    }
}

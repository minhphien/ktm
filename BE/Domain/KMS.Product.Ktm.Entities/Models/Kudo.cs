using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class Kudo : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int KudoDetailId { get; set; }

        public virtual Employee Sender { get; set; }
        public virtual Employee Receiver { get; set; }
        public virtual KudoDetail KudoDetail { get; set; }
    }
}

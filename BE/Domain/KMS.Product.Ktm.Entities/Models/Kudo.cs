using System;

namespace KMS.Product.Ktm.Entities.Models
{
    public class Kudo : BaseEntity
    {
        public string Content { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int KudoTypeId { get; set; }

        public virtual EmployeeTeam Sender { get; set; }
        public virtual EmployeeTeam Receiver { get; set; }
        public virtual KudoType KudoType { get; set; }

    }
}

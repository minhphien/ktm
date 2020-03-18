using System;

namespace KMS.Product.Ktm.Repository.Models
{
    public class Kudo
    {
        public int KudoID { get; set; }
        public string Content { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public int KudoTypeID { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual EmployeeTeam Sender { get; set; }
        public virtual EmployeeTeam Receiver { get; set; }
        public virtual KudoType KudoType { get; set; }

    }
}

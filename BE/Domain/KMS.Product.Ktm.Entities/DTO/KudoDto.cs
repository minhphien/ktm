using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.DTO
{
    public class KudoDto
    {
        // sender user name
        public string SenderUsername { get; set; }

        // reciver user name
        public string ReceiverUsername { get; set; }

        // kudo content
        public string Content { get; set; }

        // slack emoji
        public string SlackEmoji { get; set; }

        // kudo type
        public int KudoTypeId { get; set; }
    }
}

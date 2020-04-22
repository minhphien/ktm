using System;
using System.Collections.Generic;
using System.Text;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Entities.DTO
{
    /// <summary>
    /// DTO kudo for report
    /// </summary>
    public class KudoDetailDto : BaseEntity
    {
        // kudo detail content
        public string Content { get; set; }
        // kudo type
        public string TypeName { get; set; }
        // sender employee id
        public string SenderBadgeId { get; set; }
        // sender last name
        public string SenderLastName { get; set; }
        // sender first and middle name 
        public string SenderFirstMidName { get; set; }
        // sender team name
        public string SenderTeam { get; set; }
        // receiver employee id
        public string ReceiverBadgeId { get; set; }
        // receiver last name
        public string ReceiverLastName { get; set; }
        // receiver first and middle
        public string ReceiverFirstMidName { get; set; }
        // receiver team
        public string ReceiverTeam { get; set; }

    }
}

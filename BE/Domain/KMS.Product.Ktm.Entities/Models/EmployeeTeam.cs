using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Entities.Models
{
    public class EmployeeTeam : BaseEntity
    {
        public DateTime JoinedDate { get; set; }

        public DateTime? ReleseadDate { get; set; }

        public  int EmployeeID { get; set; }

        public  int TeamID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Team Team { get; set; }
    }
}

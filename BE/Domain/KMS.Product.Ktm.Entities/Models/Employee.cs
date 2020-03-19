using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMS.Product.Ktm.Entities.Models
{
    public class Employee : BaseEntity
    {
        public string EmployeeBadgeId { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string Email { get; set; }
        public string SlackAccount { get; set; }
        public DateTime JoinedDate { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Entities.Models
{
    public class Team : BaseEntity
    {
        public string TeamName { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}

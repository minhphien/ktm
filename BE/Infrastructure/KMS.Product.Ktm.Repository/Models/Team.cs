using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Repository.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }
    }
}

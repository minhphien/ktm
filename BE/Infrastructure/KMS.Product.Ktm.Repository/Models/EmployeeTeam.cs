using System;
using System.Collections.Generic;

namespace KMS.Product.Ktm.Repository.Models
{
    public class EmployeeTeam
    {
        public int EmployeeTeamID { get; set; }
        public DateTime JoinedDate { get; set; }
        public DateTime ReleseadDate { get; set; }
        public  int EmployeeID { get; set; }
        public  int TeamID { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Kudo> KudoSends { get; set; }
        public virtual ICollection<Kudo> KudoReceives { get; set; }
    }
}

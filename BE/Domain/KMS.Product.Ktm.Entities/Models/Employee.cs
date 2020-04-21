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

        public DateTime JoinedDate { get; set; }
        
        public int EmployeeRoleId { get; set; }

        public string CurrentTeam { get; set; }

        public string SlackUserId { get; set; }

        public int? MentorId { get; set; }

        public virtual EmployeeRole EmployeeRole { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; }

        public virtual ICollection<Kudo> KudoSends { get; set; }
        
        public virtual ICollection<Kudo> KudoReceives { get; set; }

        public virtual ICollection<Item> CheckListItems { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual Employee Mentor { get; set; }

        public virtual ICollection<Employee> Mentee { get; set; }
    }
}
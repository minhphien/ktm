using System;
using System.Collections.Generic;
using System.Text;

namespace KMS.Product.Ktm.Entities.Models
{
    public class EmployeeRole : BaseEntity
    {
        public string RoleName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

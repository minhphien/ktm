using System.Collections.Generic;
using KMS.Product.Ktm.Entities.Models;

namespace KMS.Product.Ktm.Services.Interfaces
{
    public interface IEmployeeService
    {
        Employee GetEmployeeById(int Id);
        void CreateEmployee(Employee employee);
    }
}

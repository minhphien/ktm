using System;
using System.Collections.Generic;
using KMS.Product.Ktm.Entities.Models;
using KMS.Product.Ktm.Services.Interfaces;

namespace KMS.Product.Ktm.Services.Implement
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void CreateEmployee(Employee employee)
        {
            _employeeRepository.Insert(employee, true);
        }

        public Employee GetEmployeeById(int Id)
        {
            return _employeeRepository.Get(Id);
        }
    }
}

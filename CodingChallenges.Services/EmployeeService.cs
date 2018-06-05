using CodingChallenges.DataAcess.Repos;
using CodingChallenges.Domains.Employees;
using System;
using System.Collections.Generic;

namespace CodingChallenges.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IRepo<Employee> _employeeRepo;

        public EmployeeService(IRepo<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public IList<Employee> GetAll()
        {
            return _employeeRepo.GetAll();
        }

    }
}

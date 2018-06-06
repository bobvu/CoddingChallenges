using CodingChallenges.Domains.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Services
{
    public interface IEmployeeService
    {
         List<Employee> GetAll();
         List<EmployeeViewModel> FindStaff(List<EmployeeViewModel> employees, long managerId)
    }
}

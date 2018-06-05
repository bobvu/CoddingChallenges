using CodingChallenges.Domains.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Services
{
    public interface IEmployeeService
    {
         IList<Employee> GetAll();
    }
}

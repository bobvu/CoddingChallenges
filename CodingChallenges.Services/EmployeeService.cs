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

        public List<Employee> GetAll()
        {
            return _employeeRepo.GetAll();
        }

        public List<EmployeeViewModel> FindStaff(List<EmployeeViewModel> employees, long managerId)
        {
            if (managerId > 0 && employees != null)
            {
                var staff = employees.FindAll(e => e.ManagerId == managerId);
                foreach (var employee in staff)
                {
                    employee.Staff = FindStaff(employees, employee.EmployeeId);
                }
                return staff;
            }else
            {
                return null;
            }
        }

    }
}

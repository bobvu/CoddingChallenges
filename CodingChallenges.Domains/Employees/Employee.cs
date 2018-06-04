using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Domains.Employees
{
    public class Employee : AuditableEntity
    {
        public Employee(string employeeName, long employeeId, long? managerId, long? id)
        {
            EmployeeName = employeeName;
            EmployeeId = employeeId;
            ManagerId = managerId;
            Id = id;
        }

        public string EmployeeName { get; set; }
        public long EmployeeId { get; set; }
        public long? ManagerId { get; set; }
        public long? Id { get; set; }
    }
}

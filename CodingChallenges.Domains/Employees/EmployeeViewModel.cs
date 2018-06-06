using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenges.Domains.Employees
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int ManagerId { get; set; }
        public List<EmployeeViewModel> Staff { get; set; }
    }
}

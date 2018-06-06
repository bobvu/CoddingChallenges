using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallenges.Domains.Users;
using CodingChallenges.Domains.Employees;
namespace CodingChallenges.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>()
                   .ForMember(d => d.Staff, map => map.Ignore());
        }
    }
}

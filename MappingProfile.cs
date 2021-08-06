using AutoMapper;
using FirmaRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>().ForMember(x => x.Id, opt => opt.Ignore());

            //CreateMap<Division, DivisionDto>();
            //CreateMap<DivisionDto, Division>().ForMember(x => x.Id, opt => opt.Ignore());

            //CreateMap<Project, ProjectDto>();
            //CreateMap<ProjectDto, Project>().ForMember(x => x.Id, opt => opt.Ignore());

            //CreateMap<Department, DepartmentDto>();
            //CreateMap<DepartmentDto, Department>().ForMember(x => x.Id, opt => opt.Ignore());

        }
    }
}

using FirmaRest.Models;
using FirmaRest.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Repository
{
    public class NodeRepository : INodeRepository
    {
        public Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies()
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesInCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisions()
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DivisionDto>> GetDivisionById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DivisionDto>> CreateDivision(DivisionDto divisionDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DivisionDto>> UpdateDivision(int id, DivisionDto divisionDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DivisionDto>> DeleteDivision(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisionsInCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectDto projectDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<ProjectDto>> DeleteProject(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInDivision(int divisionId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<DepartmentDto>> DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsInProject(int projectId)
        {
            throw new NotImplementedException();
        }
    }
}

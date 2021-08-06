using FirmaRest.Models;
using FirmaRest.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Repository
{
    interface INodeRepository
    {
        public Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies();
        public Task<ActionResult<CompanyDto>> GetCompanyById(int id);
        public Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto);
        public Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto);
        public Task<ActionResult<CompanyDto>> DeleteCompany(int id);
        public Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesInCompany(int companyId);

        public Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisions();
        public Task<ActionResult<DivisionDto>> GetDivisionById(int id);
        public Task<ActionResult<DivisionDto>> CreateDivision(DivisionDto divisionDto);
        public Task<ActionResult<DivisionDto>> UpdateDivision(int id, DivisionDto divisionDto);
        public Task<ActionResult<DivisionDto>> DeleteDivision(int id);
        public Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisionsInCompany(int companyId);

        public Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects();
        public Task<ActionResult<ProjectDto>> GetProjectById(int id);
        public Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto);
        public Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectDto projectDto);
        public Task<ActionResult<ProjectDto>> DeleteProject(int id);
        public Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInDivision(int divisionId);

        public Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments();
        public Task<ActionResult<DepartmentDto>> GetDepartmentById(int id);
        public Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto);
        public Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, DepartmentDto departmentDto);
        public Task<ActionResult<DepartmentDto>> DeleteDepartment(int id);
        public Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsInProject(int projectId);
    }

}

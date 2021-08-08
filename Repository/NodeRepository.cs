using AutoMapper;
using FirmaRest.DataValidation;
using FirmaRest.Exceptions;
using FirmaRest.Models;
using FirmaRest.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmaRest.Repository
{
    public class NodeRepository : INodeRepository
    {
        private readonly TestDBContext _context;
        private readonly IMapper _mapper;
        private readonly ErrorRaiser _errorRaiser;

        public NodeRepository(TestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _errorRaiser = new ErrorRaiser(context);
        }

        #region privateMethods

        // Private metody na zistenie ID firmy z podradneho uzla

        private int GetCompanyIdFromProject(ProjectDto project)
        {
            return (_context.Divisions.Find(project.DivisionId)).CompanyId;
        }

        private int GetCompanyIdFromDepartment(DepartmentDto department)
        {
            var project = _context.Projects.Find(department.ProjectId);
            var projectDto = _mapper.Map<ProjectDto>(project);
            return GetCompanyIdFromProject(projectDto);
        }

        #endregion privateMethods

        #region company

        // Company

        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies()
        {
            return await _context.Companies
                .Select(c => _mapper.Map<CompanyDto>(c))
                .ToListAsync();
        }
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(id);

            var company = await _context.Companies.FindAsync(id);

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto)
        {
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(companyDto.Director);
            _errorRaiser.RaiseErrorIfEmployeeNotUnemployed(companyDto.Director);

            var company = _mapper.Map<Company>(companyDto);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(id);

            var company = await _context.Companies.FindAsync(id);

            // ak sa meni riaditel firmy
            if (company.Director != companyDto.Director)
            {
                _errorRaiser.RaiseErrorIfEmployeeDoesntExist(companyDto.Director);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(companyDto.Director, companyDto.Id);
            }

            _mapper.Map(companyDto, company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(id);

            var company = await _context.Companies.FindAsync(id);

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesInCompany(int companyId)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(companyId);

            return await _context.Employees
                .Where(e => e.CompanyId == companyId)
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisionsInCompany(int companyId)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(companyId);

            return await _context.Divisions
                .Where(d => d.CompanyId == companyId)
                .Select(d => _mapper.Map<DivisionDto>(d))
                .ToListAsync();
        }

        #endregion company

        #region division
        // Division

        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisions()
        {
            return await _context.Divisions
                .Select(d => _mapper.Map<DivisionDto>(d))
                .ToListAsync();
        }
        public async Task<ActionResult<DivisionDto>> GetDivisionById(int id)
        {
            _errorRaiser.RaiseErrorIfDivisionDoesntExist(id);

            var division = await _context.Divisions.FindAsync(id);
            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> CreateDivision(DivisionDto divisionDto)
        {
            _errorRaiser.RaiseErrorIfCompanyDoesntExist(divisionDto.CompanyId);
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(divisionDto.Leader);
            _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(divisionDto.Leader, divisionDto.CompanyId);

            var division = _mapper.Map<Division>(divisionDto);

            _context.Divisions.Add(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> UpdateDivision(int id, DivisionDto divisionDto)
        {
            _errorRaiser.RaiseErrorIfDivisionDoesntExist(id);

            var division = await _context.Divisions.FindAsync(id);

            if (division.CompanyId != divisionDto.CompanyId)
            {
                _errorRaiser.RaiseErrorIfCompanyDoesntExist(divisionDto.CompanyId);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(divisionDto.Leader, divisionDto.CompanyId);
            }

            if (division.Leader != divisionDto.Leader)
            {
                _errorRaiser.RaiseErrorIfEmployeeDoesntExist(divisionDto.Leader);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(divisionDto.Leader, divisionDto.CompanyId);
            }

            _mapper.Map(divisionDto, division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> DeleteDivision(int id)
        {
            _errorRaiser.RaiseErrorIfDivisionDoesntExist(id);

            var division = await _context.Divisions.FindAsync(id);

            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInDivision(int divisionId)
        {
            _errorRaiser.RaiseErrorIfDivisionDoesntExist(divisionId);

            return await _context.Projects
                .Where(p => p.DivisionId == divisionId)
                .Select(p => _mapper.Map<ProjectDto>(p))
                .ToListAsync();
        }

        #endregion division

        #region project

        // Project

        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            return await _context.Projects
                .Select(p => _mapper.Map<ProjectDto>(p))
                .ToListAsync();
        }
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            _errorRaiser.RaiseErrorIfProjectDoesntExist(id);

            var project = await _context.Projects.FindAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            _errorRaiser.RaiseErrorIfDivisionDoesntExist(projectDto.DivisionId);
            var division = await _context.Divisions.FindAsync(projectDto.DivisionId);
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(projectDto.Leader);
            _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(projectDto.Leader, division.CompanyId);

            var project = _mapper.Map<Project>(projectDto);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectDto projectDto)
        {
            _errorRaiser.RaiseErrorIfProjectDoesntExist(id);

            var project = await _context.Projects.FindAsync(id);

            if (project.DivisionId != projectDto.DivisionId)
            {
                _errorRaiser.RaiseErrorIfDivisionDoesntExist(projectDto.DivisionId);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(projectDto.Leader, GetCompanyIdFromProject(projectDto));
            }

            if (project.Leader != projectDto.Leader)
            {
                _errorRaiser.RaiseErrorIfEmployeeDoesntExist(projectDto.Leader);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(projectDto.Leader, GetCompanyIdFromProject(projectDto));
            }

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> DeleteProject(int id)
        {
            _errorRaiser.RaiseErrorIfProjectDoesntExist(id);

            var project = await _context.Projects.FindAsync(id);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsInProject(int projectId)
        {
            _errorRaiser.RaiseErrorIfProjectDoesntExist(projectId);

            return await _context.Departments
                .Where(d => d.ProjectId == projectId)
                .Select(d => _mapper.Map<DepartmentDto>(d))
                .ToListAsync();
        }

        #endregion project

        #region department

        // Department

        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            return await _context.Departments
                .Select(d => _mapper.Map<DepartmentDto>(d))
                .ToListAsync();
        }
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            _errorRaiser.RaiseErrorIfDepartmentDoesntExist(id);

            var department = await _context.Departments.FindAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto)
        {
            _errorRaiser.RaiseErrorIfProjectDoesntExist(departmentDto.ProjectId);
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(departmentDto.Leader);
            _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));

            var department = _mapper.Map<Department>(departmentDto);

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            _errorRaiser.RaiseErrorIfDepartmentDoesntExist(id);

            var department = await _context.Departments.FindAsync(id);

            if (department.ProjectId != departmentDto.ProjectId)
            {
                _errorRaiser.RaiseErrorIfProjectDoesntExist(departmentDto.ProjectId);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));
            }

            if (department.Leader != departmentDto.Leader)
            {
                _errorRaiser.RaiseErrorIfEmployeeDoesntExist(departmentDto.Leader);
                _errorRaiser.RaiseErrorIfEmployeeNotInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));
            }

            _mapper.Map(departmentDto, department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment(int id)
        {
            _errorRaiser.RaiseErrorIfDepartmentDoesntExist(id);

            var department = await _context.Departments.FindAsync(id);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        
        #endregion department
    }
}

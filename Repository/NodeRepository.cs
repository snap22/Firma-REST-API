using AutoMapper;
using FirmaRest.DataContext;
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
        private readonly ContextChecker _checker;
        private readonly ExceptionMessageCreator _messageCreator;

        public NodeRepository(TestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _checker = new ContextChecker(context);
            _messageCreator = new ExceptionMessageCreator();
        }

        #region privateMethods

        // Pomocne metody na overenie spravnosti
        private void CheckIfCompanyExists(int companyId)
        {
            if (!_checker.CompanyExists(companyId))
                throw new NotExistsException(_messageCreator.CreateCompanyNotExistsMessage(companyId));
        }
        private void CheckIfDivisionExists(int divisionId)
        {
            if (!_checker.DivisionExists(divisionId))
                throw new NotExistsException(_messageCreator.CreateDivisionNotExistsMessage(divisionId));
        }
        private void CheckIfProjectExists(int projectId)
        {
            if (!_checker.ProjectExists(projectId))
                throw new NotExistsException(_messageCreator.CreateProjectNotExistsMessage(projectId));
        }
        private void CheckIfDepartmentExists(int departmentId)
        {
            if (!_checker.DepartmentExists(departmentId))
                throw new NotExistsException(_messageCreator.CreateDepartmentNotExistsMessage(departmentId));
        }
        private void CheckIfEmployeeExists(int employeeId)
        {
            if (!_checker.EmployeeExists(employeeId))
                throw new NotExistsException(_messageCreator.CreateEmployeeNotExistsMessage(employeeId));
        }
        private void CheckIfEmployeeInTheCompany(int employeeId, int companyId)
        {
            if (!_checker.EmployeeInTheCompany(employeeId, companyId))
                throw new EmployeeDifferentCompanyException(_messageCreator.CreateEmployeeDifferentCompanyMessage());
        }
        private void CheckIfEmployeeIsUnemployed(int employeeId)
        {
            if (!_checker.EmployeeIsUnemployed(employeeId))
                throw new EmployeeDifferentCompanyException(_messageCreator.CreateEmployeeDifferentCompanyMessage());
        }

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
            CheckIfCompanyExists(id);

            var company = await _context.Companies.FindAsync(id);

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto)
        {
            CheckIfEmployeeExists(companyDto.Director);
            CheckIfEmployeeIsUnemployed(companyDto.Director);

            var company = _mapper.Map<Company>(companyDto);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto)
        {
            CheckIfCompanyExists(id);

            var company = await _context.Companies.FindAsync(id);

            // ak sa meni riaditel firmy
            if (company.Director != companyDto.Director)
            {
                CheckIfEmployeeExists(companyDto.Director);
                CheckIfEmployeeInTheCompany(companyDto.Director, companyDto.Id);
            }

            _mapper.Map(companyDto, company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            if (!_checker.CompanyExists(id))
                throw new NotExistsException(_messageCreator.CreateCompanyNotExistsMessage(id));

            var company = await _context.Companies.FindAsync(id);

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesInCompany(int companyId)
        {
            CheckIfCompanyExists(companyId);

            return await _context.Employees
                .Where(e => e.CompanyId == companyId)
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToListAsync();
        }
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisionsInCompany(int companyId)
        {
            CheckIfCompanyExists(companyId);

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
            CheckIfDivisionExists(id);

            var division = await _context.Divisions.FindAsync(id);
            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> CreateDivision(DivisionDto divisionDto)
        {
            CheckIfCompanyExists(divisionDto.CompanyId);
            CheckIfEmployeeExists(divisionDto.Leader);
            CheckIfEmployeeInTheCompany(divisionDto.Leader, divisionDto.CompanyId);

            var division = _mapper.Map<Division>(divisionDto);

            _context.Divisions.Add(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> UpdateDivision(int id, DivisionDto divisionDto)
        {
            CheckIfDivisionExists(id);

            var division = await _context.Divisions.FindAsync(id);

            if (division.CompanyId != divisionDto.CompanyId)
            {
                CheckIfCompanyExists(divisionDto.CompanyId);
                CheckIfEmployeeInTheCompany(divisionDto.Leader, divisionDto.CompanyId);
            }

            if (division.Leader != divisionDto.Leader)
            {
                CheckIfEmployeeExists(divisionDto.Leader);
                CheckIfEmployeeInTheCompany(divisionDto.Leader, divisionDto.CompanyId);
            }

            _mapper.Map(divisionDto, division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> DeleteDivision(int id)
        {
            CheckIfDivisionExists(id);

            var division = await _context.Divisions.FindAsync(id);

            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }

        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInDivision(int divisionId)
        {
            CheckIfDivisionExists(divisionId);

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
            CheckIfProjectExists(id);

            var project = await _context.Projects.FindAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            CheckIfDivisionExists(projectDto.DivisionId);
            var division = await _context.Divisions.FindAsync(projectDto.DivisionId);
            CheckIfEmployeeExists(projectDto.Leader);
            CheckIfEmployeeInTheCompany(projectDto.Leader, division.CompanyId);

            var project = _mapper.Map<Project>(projectDto);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectDto projectDto)
        {
            CheckIfProjectExists(id);

            var project = await _context.Projects.FindAsync(id);

            if (project.DivisionId != projectDto.DivisionId)
            {
                CheckIfDivisionExists(projectDto.DivisionId);
                CheckIfEmployeeInTheCompany(projectDto.Leader, GetCompanyIdFromProject(projectDto));
            }

            if (project.Leader != projectDto.Leader)
            {
                CheckIfEmployeeExists(projectDto.Leader);
                CheckIfEmployeeInTheCompany(projectDto.Leader, GetCompanyIdFromProject(projectDto));
            }

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> DeleteProject(int id)
        {
            CheckIfProjectExists(id);

            var project = await _context.Projects.FindAsync(id);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsInProject(int projectId)
        {
            CheckIfProjectExists(projectId);

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
            CheckIfDepartmentExists(id);

            var department = await _context.Departments.FindAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto)
        {
            CheckIfProjectExists(departmentDto.ProjectId);
            CheckIfEmployeeExists(departmentDto.Leader);
            CheckIfEmployeeInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));

            var department = _mapper.Map<Department>(departmentDto);

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            CheckIfDepartmentExists(id);

            var department = await _context.Departments.FindAsync(id);

            if (department.ProjectId != departmentDto.ProjectId)
            {
                CheckIfProjectExists(departmentDto.ProjectId);
                CheckIfEmployeeInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));
            }

            if (department.Leader != departmentDto.Leader)
            {
                CheckIfEmployeeExists(departmentDto.Leader);
                CheckIfEmployeeInTheCompany(departmentDto.Leader, GetCompanyIdFromDepartment(departmentDto));
            }

            _mapper.Map(departmentDto, department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment(int id)
        {
            CheckIfDepartmentExists(id);

            var department = await _context.Departments.FindAsync(id);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        
        #endregion department
    }
}

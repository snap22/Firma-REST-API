using AutoMapper;
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
        public NodeRepository(TestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // Company

        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetAllCompanies()
        {
            return await _context.Companies
                .Select(c => _mapper.Map<CompanyDto>(c))
                .ToListAsync();
        }
        public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> UpdateCompany(int id, CompanyDto companyDto)
        {
            var company = await _context.Companies.FindAsync(id);

            _mapper.Map(companyDto, company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return _mapper.Map<CompanyDto>(company);
        }
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesInCompany(int companyId)
        {
            return await _context.Employees
                .Where(e => e.CompanyId == companyId)
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToListAsync();
        }



        // Division

        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisions()
        {
            return await _context.Divisions
                .Select(d => _mapper.Map<DivisionDto>(d))
                .ToListAsync();
        }
        public async Task<ActionResult<DivisionDto>> GetDivisionById(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> CreateDivision(DivisionDto divisionDto)
        {
            var division = _mapper.Map<Division>(divisionDto);

            _context.Divisions.Add(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> UpdateDivision(int id, DivisionDto divisionDto)
        {
            var division = await _context.Divisions.FindAsync(id);

            _mapper.Map(divisionDto, division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<DivisionDto>> DeleteDivision(int id)
        {
            var division = await _context.Divisions.FindAsync(id);

            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();

            return _mapper.Map<DivisionDto>(division);
        }
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetAllDivisionsInCompany(int companyId)
        {
            return await _context.Divisions
                .Where(d => d.CompanyId == companyId)
                .Select(d => _mapper.Map<DivisionDto>(d))
                .ToListAsync();
        }



        // Project

        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjects()
        {
            return await _context.Projects
                .Select(p => _mapper.Map<ProjectDto>(p))
                .ToListAsync();
        }
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> CreateProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> UpdateProject(int id, ProjectDto projectDto)
        {
            var project = await _context.Projects.FindAsync(id);

            _mapper.Map(projectDto, project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<ProjectDto>> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAllProjectsInDivision(int divisionId)
        {
            return await _context.Projects
                .Where(p => p.DivisionId == divisionId)
                .Select(p => _mapper.Map<ProjectDto>(p))
                .ToListAsync();
        }



        // Department

        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments()
        {
            return await _context.Departments
                .Select(d => _mapper.Map<DepartmentDto>(d))
                .ToListAsync();
        }
        public async Task<ActionResult<DepartmentDto>> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> CreateDepartment(DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);

            _mapper.Map(departmentDto, department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return _mapper.Map<DepartmentDto>(department);
        }
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartmentsInProject(int projectId)
        {
            return await _context.Departments
                .Where(d => d.ProjectId == projectId)
                .Select(d => _mapper.Map<DepartmentDto>(d))
                .ToListAsync();
        }
    }
}

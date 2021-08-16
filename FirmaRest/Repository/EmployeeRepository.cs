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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly TestDBContext _context;
        private readonly IMapper _mapper;
        private readonly ErrorRaiser _errorRaiser;

        public EmployeeRepository(TestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _errorRaiser = new ErrorRaiser(context);
        }


        

        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            return await _context.Employees
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToListAsync();
        }
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(id);

            var employee = await _context.Employees.FindAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetUnemployed()
        {
            return await _context.Employees
                .Where(e => e.CompanyId == null)
                .Select(e => _mapper.Map<EmployeeDto>(e))    // skonvertovat az pred ToList !
                .ToListAsync();
        }
        public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            
            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(id);

            var employee = await _context.Employees.FindAsync(id);

            if (employeeDto.CompanyId != employee.CompanyId)
            {
                _errorRaiser.RaiseErrorIfEmployeeLeaderOfAnyNode(id, toDelete: false);
            }

            _mapper.Map(employeeDto, employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }
        public async Task<ActionResult<EmployeeDto>> DeleteEmployee(int id)
        {
            _errorRaiser.RaiseErrorIfEmployeeDoesntExist(id);

            var employee = await _context.Employees.FindAsync(id);

            _errorRaiser.RaiseErrorIfEmployeeLeaderOfAnyNode(id, toDelete: true);

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

    }
}

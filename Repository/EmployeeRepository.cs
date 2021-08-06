using AutoMapper;
using FirmaRest.Models;
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

        public EmployeeRepository(TestDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees()
        {
            return await _context.Employees
                .Select(e => _mapper.Map<EmployeeDto>(e))
                .ToListAsync();
        }
        public Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<IEnumerable<EmployeeDto>>> GetUnemployed()
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
        public Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }
        public Task<ActionResult<EmployeeDto>> DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }
        public bool CheckIfEmployeeExists(int id)
        {
            throw new NotImplementedException();
        }




    }
}

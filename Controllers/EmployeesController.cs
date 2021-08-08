using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Repository;
using FirmaRest.Models.DTO;
using FirmaRest.Exceptions;

namespace FirmaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            return await _repository.GetAllEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            try
            {
                return await _repository.GetEmployeeById(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Employees/Unemployed
        [HttpGet]
        [Route("Unemployed")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetUnemployedEmployees()
        {
            return await _repository.GetUnemployed();
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDto employeeDto)
        {
            try
            {
                await _repository.UpdateEmployee(id, employeeDto);
                return NoContent();
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EmployeeDifferentCompanyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employee = await _repository.CreateEmployee(employeeDto);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Value.Id }, employee);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployee(int id)
        {
            try
            {
                return await _repository.DeleteEmployee(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
            catch (CannotDeleteException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

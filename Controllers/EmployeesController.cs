using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Models;
using FirmaRest.Repository;

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
            var employee = await _repository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
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
            if (id != employeeDto.Id)
            {
                return BadRequest();
            }

            var employee = await _repository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _repository.UpdateEmployee(id, employeeDto);

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!EmployeeExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {

            var employee = await _repository.CreateEmployee(employeeDto);


            //return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Value.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployee(int id)
        {
            var employee = await _repository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }

            var deletedEmployee = await _repository.DeleteEmployee(id);

            return deletedEmployee;
        }

        

        

        


    }
}

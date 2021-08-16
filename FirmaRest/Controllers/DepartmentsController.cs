using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Models;
using FirmaRest.Models.DTO;
using FirmaRest.Repository;
using FirmaRest.Exceptions;

namespace FirmaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly INodeRepository _repository;

        public DepartmentsController(INodeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartment()
        {
            return await _repository.GetAllDepartments();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            try
            {
                return await _repository.GetDepartmentById((id));
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, DepartmentDto departmentDto)
        {
            try
            {
                await _repository.UpdateDepartment(id, departmentDto);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EmployeeDifferentCompanyException ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment(DepartmentDto departmentDto)
        {
            try
            {
                var department = await _repository.CreateDepartment(departmentDto);
                return CreatedAtAction(nameof(GetDepartment), new { id = department.Value.Id }, department);
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

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentDto>> DeleteDepartment(int id)
        {
            try
            {
                return await _repository.DeleteDepartment(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

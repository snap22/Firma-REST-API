using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Models;
using Microsoft.Data.SqlClient;
using FirmaRest.Models.DTO;
using FirmaRest.Repository;
using FirmaRest.Exceptions;

namespace FirmaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly INodeRepository _repository;

        public CompaniesController(INodeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            return await _repository.GetAllCompanies();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            try
            {
                var company = await _repository.GetCompanyById(id);
                return company;
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Companies/id/Employees
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("{id}/Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesOfCompany(int id)
        {
            try
            {
                return await _repository.GetAllEmployeesInCompany(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }        
        }
        
        // GET: api/Companies/id/Divisions
        [HttpGet]
        [Route("{id}/Divisions")]
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetDivisionsOfCompany(int id)
        {
            try
            {
                return await _repository.GetAllDivisionsInCompany(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message); ;
            }
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyDto companyDto)
        {
            try
            {
                await _repository.UpdateCompany(id, companyDto);
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

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyDto companyDto)
        {
            try
            {
                var company = await _repository.CreateCompany(companyDto);
                return CreatedAtAction(nameof(GetCompany), new { id = company.Value.Id }, company);
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

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            try
            {
                return await _repository.DeleteCompany(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

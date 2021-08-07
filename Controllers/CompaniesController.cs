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
            var company = await _repository.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // GET: api/Companies/id/Employees
        //[HttpGet("{id}")]
        [HttpGet]
        [Route("{id}/Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesOfCompany(int id)
        {
            var company = await _repository.GetCompanyById(id);

            if (company == null)
            {
                return NotFound();
            }

            return await _repository.GetAllEmployeesInCompany(id);
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CompanyDto companyDto)
        {
            if (id != companyDto.Id)
            {
                return BadRequest();
            }

            var company = await _repository.UpdateCompany(id, companyDto);

            if (company == null)
                return NotFound();

            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyDto companyDto)
        {
            var company = await _repository.CreateCompany(companyDto);

            return CreatedAtAction("GetCompany", new { id = company.Value.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompanyDto>> DeleteCompany(int id)
        {
            var company = await _repository.GetCompanyById(id);

            if (company == null)
                return NotFound();

            return await _repository.DeleteCompany(id);
        }



        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Models;
using FirmaRest.Exceptions;
using FirmaRest.Repository;
using FirmaRest.Models.DTO;

namespace FirmaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {
        private readonly INodeRepository _repository;

        public DivisionsController(INodeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Divisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DivisionDto>>> GetDivisions()
        {
            return await _repository.GetAllDivisions();
        }

        // GET: api/Divisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DivisionDto>> GetDivision(int id)
        {
            try
            {
                return await _repository.GetDivisionById(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Divisions/id/Projects
        [HttpGet]
        [Route("{id}/Projects")]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsOfDivision(int id)
        {
            try
            {
                return await _repository.GetAllProjectsInDivision(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message); ;
            }
        }

        // PUT: api/Divisions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDivision(int id, DivisionDto divisionDto)
        {
            try
            {
                await _repository.UpdateDivision(id, divisionDto);
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

        // POST: api/Divisions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DivisionDto>> PostDivision(DivisionDto divisionDto)
        {
            try
            {
                var division = await _repository.CreateDivision(divisionDto);
                return CreatedAtAction(nameof(GetDivision), new { id = division.Value.Id }, division);
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

        // DELETE: api/Divisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DivisionDto>> DeleteDivision(int id)
        {
            try
            {
                return await _repository.DeleteDivision(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

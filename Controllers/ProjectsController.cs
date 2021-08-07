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
    public class ProjectsController : ControllerBase
    {
        private readonly INodeRepository _repository;

        public ProjectsController(INodeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProject()
        {
            return await _repository.GetAllProjects();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            try
            {
                return await _repository.GetProjectById(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Projects/id/Departments
        [HttpGet]
        [Route("{id}/Departments")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartmentsOfProject(int id)
        {
            try
            {
                return await _repository.GetAllDepartmentsInProject(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message); ;
            }
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDto projectDto)
        {
            try
            {
                await _repository.UpdateProject(id, projectDto);
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

        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> PostProject(ProjectDto projectDto)
        {
            try
            {
                var project = await _repository.CreateProject(projectDto);
                return CreatedAtAction(nameof(GetProject), new { id = project.Value.Id }, project);
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

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectDto>> DeleteProject(int id)
        {
            try
            {
                return await _repository.DeleteProject(id);
            }
            catch (NotExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirmaRest.Models;

namespace FirmaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionsController : ControllerBase
    {
        private readonly TestDBContext _context;

        public DivisionsController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/Divisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Division>>> GetDivisions()
        {
            return await _context.Divisions.ToListAsync();
        }

        // GET: api/Divisions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Division>> GetDivision(int id)
        {
            var division = await _context.Divisions.FindAsync(id);

            if (division == null)
            {
                return NotFound();
            }

            return division;
        }

        // PUT: api/Divisions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDivision(int id, Division division)
        {
            if (id != division.Id)
            {
                return BadRequest();
            }

            _context.Entry(division).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Divisions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Division>> PostDivision(Division division)
        {
            _context.Divisions.Add(division);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDivision", new { id = division.Id }, division);
        }

        // DELETE: api/Divisions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Division>> DeleteDivision(int id)
        {
            var division = await _context.Divisions.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            _context.Divisions.Remove(division);
            await _context.SaveChangesAsync();

            return division;
        }

        private bool DivisionExists(int id)
        {
            return _context.Divisions.Any(e => e.Id == id);
        }
    }
}

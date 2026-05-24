using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderAPI.Models;

namespace PcBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildComponentsController : ControllerBase
    {
        private readonly PcBuilderContext _context;

        public BuildComponentsController(PcBuilderContext context)
        {
            _context = context;
        }

        // GET: api/BuildComponents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildComponent>>> GetBuildComponents()
        {
            return await _context.BuildComponents.ToListAsync();
        }

        // GET: api/BuildComponents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildComponent>> GetBuildComponent(int id)
        {
            var buildComponent = await _context.BuildComponents.FindAsync(id);

            if (buildComponent == null)
            {
                return NotFound();
            }

            return buildComponent;
        }

        // PUT: api/BuildComponents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuildComponent(int id, BuildComponent buildComponent)
        {
            if (id != buildComponent.Id)
            {
                return BadRequest();
            }

            _context.Entry(buildComponent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildComponentExists(id))
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

        // POST: api/BuildComponents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuildComponent>> PostBuildComponent(BuildComponent buildComponent)
        {
            _context.BuildComponents.Add(buildComponent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildComponent", new { id = buildComponent.Id }, buildComponent);
        }

        // DELETE: api/BuildComponents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuildComponent(int id)
        {
            var buildComponent = await _context.BuildComponents.FindAsync(id);
            if (buildComponent == null)
            {
                return NotFound();
            }

            _context.BuildComponents.Remove(buildComponent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildComponentExists(int id)
        {
            return _context.BuildComponents.Any(e => e.Id == id);
        }
    }
}

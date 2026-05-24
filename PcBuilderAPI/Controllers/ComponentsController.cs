using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderAPI.Models;

namespace PcBuilderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly PcBuilderContext _context;

        public ComponentsController(PcBuilderContext context)
        {
            _context = context;
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
        {
            return await _context.Components.AsNoTracking().ToListAsync();
        }

        // GET: api/Components
        [HttpGet("{id}")]
        public async Task<ActionResult<Component>> GetComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);

            if (component == null)
            {
                return NotFound();
            }

            return component;
        }

        // PUT: api/Components
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.Id)
            {
                return BadRequest("ID не збігаються");
            }

            var existingComponent = await _context.Components.FindAsync(id);
            if (existingComponent == null)
            {
                return NotFound();
            }

            existingComponent.Name = component.Name;
            existingComponent.Description = component.Description;
            existingComponent.Price = component.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(id))
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

        // POST: api/Components
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }

        // DELETE: api/Components
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound($"Компонент з ID {id} не знайдено");
            }

            _context.Components.Remove(component);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
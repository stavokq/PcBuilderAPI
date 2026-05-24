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
    public class ComponentsController : ControllerBase
    {
        private readonly PcBuilderContext _context;

        public ComponentsController(PcBuilderContext context)
        {
            _context = context;

            if (!_context.Categories.Any())
            {
                var cpuCategory = new Category { Name = "Процесори" };
                var gpuCategory = new Category { Name = "Відеокарти" };

                var intel = new Manufacturer { Name = "Intel", Website = "https://intel.com" };
                var nvidia = new Manufacturer { Name = "NVIDIA", Website = "https://nvidia.com" };

                _context.Categories.AddRange(cpuCategory, gpuCategory);
                _context.Manufacturers.AddRange(intel, nvidia);
                _context.SaveChanges();

                _context.Components.AddRange(
                    new Component
                    {
                        Name = "Intel Core i5-14600K",
                        Description = "3.5GHz up to 5.3GHz, 14 cores, 24MB cache.",
                        Price = 13500.00m,
                        Category = cpuCategory,
                        Manufacturer = intel
                    },
                    new Component
                    {
                        Name = "NVIDIA GeForce RTX 5070",
                        Description = "12GB GDDR7.",
                        Price = 28000.00m,
                        Category = gpuCategory,
                        Manufacturer = nvidia
                    }
                );
                _context.SaveChanges();
            }
        }

        // GET: api/Components
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Component>>> GetComponents()
        {
            return await _context.Components.ToListAsync();
        }

        // GET: api/Components/5
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

        // PUT: api/Components/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponent(int id, Component component)
        {
            if (id != component.Id)
            {
                return BadRequest();
            }

            _context.Entry(component).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Component>> PostComponent(Component component)
        {
            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponent", new { id = component.Id }, component);
        }

        // DELETE: api/Components/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponent(int id)
        {
            var component = await _context.Components.FindAsync(id);
            if (component == null)
            {
                return NotFound();
            }

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}

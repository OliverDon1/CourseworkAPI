using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseworkAPI_PLEASEKILLME_.Models;

namespace CourseworkAPI_PLEASEKILLME_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivesController : ControllerBase
    {
        private readonly COMP2001_ODonnellyContext _context;

        public ArchivesController(COMP2001_ODonnellyContext context)
        {
            _context = context;
        }
        
        

        // GET: api/Archives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archive>>> GetArchives()
        {
            return await _context.Archives.ToListAsync();
        }

        // GET: api/Archives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Archive>> GetArchive(int id)
        {
            var archive = await _context.Archives.FindAsync(id);

            if (archive == null)
            {
                return NotFound();
            }

            return archive;
        }

        // PUT: api/Archives/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchive(int id, Archive archive)
        {
            if (id != archive.ArchiveId)
            {
                return BadRequest();
            }

            _context.Entry(archive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchiveExists(id))
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

        // POST: api/Archives
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Archive>> PostArchive(Archive archive)
        {
            _context.Archives.Add(archive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchive", new { id = archive.ArchiveId }, archive);
        }

        // DELETE: api/Archives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchive(int id)
        {
            var archive = await _context.Archives.FindAsync(id);
            if (archive == null)
            {
                return NotFound();
            }

            _context.Archives.Remove(archive);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArchiveExists(int id)
        {
            return _context.Archives.Any(e => e.ArchiveId == id);
        }
    }
}

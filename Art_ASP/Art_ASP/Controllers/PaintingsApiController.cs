using Art_ASP.Data;
using Art_ASP.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintingsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaintingsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Painting>>> GetPaintings()
        {
            return await _context.Paintings.Where(p => !p.IsDeleted).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Painting>> PostPainting([FromBody] Painting painting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Paintings.Add(painting);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaintingById), new { id = painting.Id }, painting);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Painting>> GetPaintingById(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return painting;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePainting(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            painting.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Art_ASP.Data;

namespace MyProject.Controllers
{
    public class PaintingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaintingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var paintings = _context.Paintings.Where(p => !p.IsDeleted).ToList();
            var deletedPaintings = _context.Paintings.Where(p => p.IsDeleted).ToList();

            ViewBag.DeletedPaintings = deletedPaintings;
            return View(paintings);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Painting painting)
        {
            if (ModelState.IsValid)
            {
                _context.Paintings.Add(painting);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(painting);
        }

        public IActionResult Delete(int id)
        {
            var painting = _context.Paintings.Find(id);
            if (painting != null)
            {
                painting.IsDeleted = true; 
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restore(int id)
        {
            var painting = _context.Paintings.FirstOrDefault(p => p.Id == id && p.IsDeleted);
            if (painting == null)
            {
                return NotFound();
            }

            painting.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PermanentlyDelete(int id)
        {
            var painting = _context.Paintings.FirstOrDefault(p => p.Id == id && p.IsDeleted);
            if (painting == null)
            {
                return NotFound();
            }

            _context.Paintings.Remove(painting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}

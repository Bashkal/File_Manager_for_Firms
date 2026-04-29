using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Controllers
{
    public class GuidesController : Controller
    {
        private readonly DataBaseContext _context;
        public GuidesController(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Guides.Include(g => g.Department).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _context.Guides
                .Include(g => g.Department)
                .Include(g => g.VideoChapters) // chapter'ları da yükle
                .FirstOrDefaultAsync(g => g.Id == id);

            if (model == null)
                return NotFound();

            return View(model);
        }

        public async Task<IActionResult> Search(string? search)
        {
            var model = await _context.Guides
                .Include(g => g.Department)
                .Where(p => p.Name.Contains(search)
                         || p.Description.Contains(search)
                         || p.Department.Name.Contains(search))
                .ToListAsync();
            return View(model);
        }
    }
}

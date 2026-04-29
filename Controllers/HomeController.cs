using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Controllers
{
    public class HomeController : Controller

    {
        private readonly DataBaseContext _context;
        public HomeController(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Guides.Include(g => g.Department);
            return View(await dataBaseContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public FileStreamResult FileDown(int id)
        {
            var guide = _context.Guides.Find(id);
            if (guide == null || string.IsNullOrEmpty(guide.File))
            {
                throw new FileNotFoundException("Kýlavuz dosyasý bulunamadý.");
            }
            else
            {
                var extension = Path.GetExtension(guide.File);
                var stream = new FileStream("wwwroot" + guide.File, FileMode.Open);
                return new FileStreamResult(stream, "application/" + extension) { FileDownloadName = Path.Combine(guide.Name, guide.FileType) };
            }
        }
    }
}

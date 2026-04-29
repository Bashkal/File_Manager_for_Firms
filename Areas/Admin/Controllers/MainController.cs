using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MainController : Controller
    {
        DataBaseContext _context;
        public MainController(DataBaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context);
        }
        public IActionResult UserInfo()
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if (user == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public FileStreamResult FileDown(int id)
        {
            var guide = _context.Guides.Find(id);
            if (guide == null || string.IsNullOrEmpty(guide.File))
            {
                throw new FileNotFoundException("Kılavuz dosyası bulunamadı.");
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

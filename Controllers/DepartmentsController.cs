using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DataBaseContext _context;
        public DepartmentsController(DataBaseContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            var department = _context.Departments.Find(id);
            var guides = _context.Guides.Include(g=>g.Department).Where(g => g.DepartmentId ==id).ToList();
            ViewData["Dep"] = department.Name;
            return View(guides);
        }
    }
}

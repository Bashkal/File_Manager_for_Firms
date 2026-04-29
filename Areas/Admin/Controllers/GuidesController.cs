using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class GuidesController : Controller
    {
        private readonly DataBaseContext _context;

        public GuidesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Guides
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Guides.Include(g => g.Department);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Admin/Guides/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guide = await _context.Guides
                .Include(g => g.Department).Include(g => g.VideoChapters.Where(a=>a.GuideId==id))
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (guide == null)
            {
                return NotFound();
            }

            return View(guide);
        }

        // GET: Admin/Guides/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Admin/Guides/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,IsActive,DepartmentId")] Guide guide, IFormFile? Image, IFormFile? File)
        {
            guide.CreatedDate = DateTime.Now;
            guide.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (Image is not null)
                {
                    string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "guides");
                    if (!Directory.Exists(ImageDirectory))
                    {
                        Directory.CreateDirectory(ImageDirectory);
                    }
                    string imageName = $"{Guid.NewGuid()}_{Image.FileName}";
                    string imagePath = Path.Combine(ImageDirectory, imageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                    guide.Image = $"/images/guides/{imageName}";
                }
                if (File is not null)
                {
                    string fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "guides");
                    if (!Directory.Exists(fileDirectory))
                    {
                        Directory.CreateDirectory(fileDirectory);
                    }
                    var extension = Path.GetExtension(File.FileName);
                    string fileName = $"{Guid.NewGuid()}_{File.FileName}";
                    string filePath = Path.Combine(fileDirectory, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await File.CopyToAsync(stream);
                    }
                    guide.File = $"/files/guides/{fileName}";
                    guide.FileType = extension;
                }


                _context.Add(guide);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", guide.DepartmentId);
            return View(guide);
        }

        // GET: Admin/Guides/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guide = await _context.Guides.FindAsync(id);
            if (guide == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", guide.DepartmentId);
            return View(guide);
        }

        // POST: Admin/Guides/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Guide guide, IFormFile? Image, IFormFile? File,TimeSpan Chapter)
        {
            if (id != guide.Id)
            {
                return NotFound();
            }
            guide.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        string ImageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "guides");
                        if (!Directory.Exists(ImageDirectory))
                        {
                            Directory.CreateDirectory(ImageDirectory);
                        }
                        string imageName = $"{Guid.NewGuid()}_{Image.FileName}";
                        string imagePath = Path.Combine(ImageDirectory, imageName);
                        using (var stream = new FileStream(imagePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        guide.Image = $"/images/guides/{imageName}";
                    }
                    if (File is not null)
                    {
                        string fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "guides");
                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        var extension = Path.GetExtension(File.FileName);
                        string fileName = $"{Guid.NewGuid()}_{File.FileName}";
                        string filePath = Path.Combine(fileDirectory, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }
                        guide.File = $"/files/guides/{fileName}";
                        guide.FileType = extension;
                    }
                    _context.Update(guide);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuideExists(guide.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", guide.DepartmentId);
            return View(guide);
        }

        // GET: Admin/Guides/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guide = await _context.Guides
                .Include(g => g.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guide == null)
            {
                return NotFound();
            }

            return View(guide);
        }

        // POST: Admin/Guides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guide = await _context.Guides.FindAsync(id);
            
            if (guide != null)
            {
                if (guide.Image is not null)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot"+ guide.Image);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    
                }
                if (guide.File is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"+ guide.File);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    
                }
                
                _context.Guides.Remove(guide);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuideExists(int id)
        {
            return _context.Guides.Any(e => e.Id == id);
        }
       
        public FileStreamResult FileDown(int id)
        {
            var guide =_context.Guides.Find(id);
            if (guide == null|| string.IsNullOrEmpty(guide.File))
            {
                    throw new FileNotFoundException("Kılavuz dosyası bulunamadı.");
            }
            else
            {   var extension = Path.GetExtension(guide.File);
                var stream = new FileStream("wwwroot"+guide.File,FileMode.Open);
                return new FileStreamResult(stream, "application/"+extension) { FileDownloadName=Path.Combine(guide.Name,guide.FileType)};
            }
            
                
           
        }
        [HttpPost]
        public IActionResult AddChapter([FromBody] ChapterDto dto)
        {
            if (dto == null || dto.GuideId <= 0)
                return BadRequest(new { message = "Geçersiz veri." });

            var chapter = new VideoChapter
            {
                GuideId = dto.GuideId,
                StartTime = TimeSpan.FromSeconds(dto.TimeInSeconds),
                Title = dto.Title ?? $"Chapter - {dto.TimeInSeconds:F0}sn"
            };

            _context.VideoChapters.Add(chapter);
            _context.SaveChanges();

            return Json(new { message = "Chapter başarıyla eklendi." });
        }

        public class ChapterDto
        {
            public int GuideId { get; set; }
            public double TimeInSeconds { get; set; }
            public string? Title { get; set; }
        }

    }
}

using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Controllers
{
    public class LoginController : Controller
    {
        private readonly DataBaseContext _context;
        public LoginController(DataBaseContext context)
        {
            _context = context;
        }
        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                // If authenticated, redirect to the main admin page
                return RedirectToAction("Main", "Admin");
            }
             return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive);
                if (user == null)
                {
                    TempData["Error"] = "Kullanıcı Bulunamadı ya da Hesap Pasif Konuma Getirilmiş";
                }
                else
                {
                    
                    var rights = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };
                    var identity = new ClaimsIdentity(rights, "Login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    if (user.IsAdmin)
                    {
                        return RedirectToAction("Main", "Admin");
                    }
                    else
                    {
                        TempData["Error"] = "Yönetici paneline erişim izniniz yok.";
                        
                    }
                }



            }
            catch (Exception Error)
            {
                //Hata logları
                throw;
            }
            return View();
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
           
            return Redirect("/");
        }
    }
}

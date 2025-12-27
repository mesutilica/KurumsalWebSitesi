using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KurumsalWebSitesi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var kullanici = _context.Users.FirstOrDefault(x => x.IsActive && x.Email == user.Email && x.Password == user.Password);
            if (kullanici != null)
            {
                var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Email, kullanici.Email), // claim = hak(kullanıcıya tanımlalan haklar)
                        new(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User") // giriş yapan kullanıcı admin ise admin yetkisiyle değilse user yetkisiyle giriş yasın.
                    };
                var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcı için bir kimlik oluşturduk
                ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
                HttpContext.SignInAsync(claimsPrincipal); // yukardaki yetkilerle sisteme giriş yaptık
                if (!string.IsNullOrEmpty(HttpContext.Request.Query["ReturnUrl"]))
                {
                    return Redirect(HttpContext.Request.Query["ReturnUrl"].ToString());
                }
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            return View();
        }
    }
}

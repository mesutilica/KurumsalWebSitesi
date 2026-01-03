using KurumsalWebSitesi.Core.Entities;
using KurumsalWebSitesi.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("kullaniciId") == null)
            {
                return RedirectToAction("Logout");
            }
            var kullanici = _context.Users.FirstOrDefault(x => x.IsActive && x.Id == HttpContext.Session.GetInt32("kullaniciId"));
            if (kullanici == null)
            {
                return NotFound("Kullanıcı Bulunamadı ya da Üyelik Pasif Edilmiş!");
            }
            return View(kullanici);
        }
        [HttpPost, Authorize]
        public IActionResult Index(User user)
        {
            if (HttpContext.Session.GetInt32("kullaniciId") == null)
            {
                return RedirectToAction("Logout");
            }
            var kullanici = _context.Users.FirstOrDefault(x => x.IsActive && x.Id == HttpContext.Session.GetInt32("kullaniciId"));
            if (kullanici == null)
            {
                return NotFound("Kullanıcı Bulunamadı ya da Üyelik Pasif Edilmiş!");
            }
            kullanici.Name = user.Name;
            kullanici.Surname = user.Surname;
            kullanici.Email = user.Email;
            kullanici.Password = user.Password;
            var sonuc = _context.SaveChanges();
            if (sonuc > 0)
            {
                TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Güncelleme Başarılı!</strong>
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Kayıt Başarısız!");
            return View(user);
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
                HttpContext.Session.SetInt32("kullaniciId", kullanici.Id);
                var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Name, kullanici.Name),
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
            try
            {
                user.IsActive = true;
                user.IsAdmin = false;
                _context.Users.Add(user);
                var sonuc = _context.SaveChanges();
                if (sonuc > 0)
                {
                    TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Kayıt Başarılı!</strong> Üye girişi yaparak size özel fırsatlardan yararlanabilirsiniz.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Kayıt Başarısız!");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(user);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace KurumsalWebSitesi.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Soyadı"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Surname { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Password { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}

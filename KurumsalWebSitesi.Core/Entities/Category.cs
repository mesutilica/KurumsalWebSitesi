using System.ComponentModel.DataAnnotations;

namespace KurumsalWebSitesi.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), StringLength(50), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Kategori Açıklama"), DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "Kategori Resmi"), StringLength(150)]
        public string? Image { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)] // ScaffoldColumn(false) : sayfa oluştururken bu kolon oluşmasın
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Durum")]
        public bool IsActive { get; set; }
        public IList<Product>? Products { get; set; }
    }
}

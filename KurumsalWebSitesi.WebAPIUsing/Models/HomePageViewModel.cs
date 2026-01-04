using KurumsalWebSitesi.Core.Entities;

namespace KurumsalWebSitesi.WebAPIUsing.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider>? Sliders { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}

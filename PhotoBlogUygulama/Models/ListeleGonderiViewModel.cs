using PhotoBlogUygulama.Data;
using System.ComponentModel.DataAnnotations;

namespace FotoBlog.Models
{
    public class ListeleGonderiViewModel
    {
        
        public List<Gonderi> Gonderiler { get; set; } = null!;

        public int SayfaNumarasi { get; set; } 

        public int ToplamSayfaSayisi { get; set; }

        public int  ToplamGonderiSayisi { get; set; }

        [Display(Name = " Arama Sözcüğü")]
        public string AramaMetni { get; set; } = null!;
    }
}

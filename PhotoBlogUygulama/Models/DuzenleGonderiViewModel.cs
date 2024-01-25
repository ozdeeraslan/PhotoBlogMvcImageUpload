using PhotoBlogUygulama.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FotoBlog.Models
{
    public class DuzenleGonderiViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage = "{0} alanı zorunludur!")]
        public string Baslik { get; set; } = null!;

        [Required(ErrorMessage = "{0} koyulması zorunludur!")]
        public string ResimYolu { get; set; } = null!;

        [Display(Name = "Farklı Bir Resim Ekle")]
        [GecerliResim(MaxDosyaBoyutuMB = 1.2)]
        public IFormFile? Resim { get; set; } = null!;
    }
}





using PhotoBlogUygulama.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PhotoBlogUygulama.Models
{
    public class YeniGonderiViewModel
    {

        [Display(Name = "Gönderi Basligi")]
        [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        public string Baslik { get; set; } = null!;

        [Display(Name = "Resim")]
        [Required(ErrorMessage = "{0} yüklemek zorunludur.")]
        [GecerliResim(MaxDosyaBoyutuMb = 1.2)]
        public IFormFile Resim { get; set; } = null!;
    }
}

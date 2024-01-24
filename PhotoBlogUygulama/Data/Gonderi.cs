using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoBlogUygulama.Data
{
    public class Gonderi
    {
        public int Id { get; set; }

        [Display(Name = "Gönderi Basligi")]
        [Required(ErrorMessage = "{0} girilmesi zorunludur.")]
        public string Baslik { get; set; } = null!;

        [Display(Name = "Resim")]
        [Required(ErrorMessage = "{0} yüklemek zorunludur.")]
        [MaxLength(255)]
        public string ResimYolu { get; set; } = null!;


    }
}

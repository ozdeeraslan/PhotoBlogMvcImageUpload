using Microsoft.AspNetCore.Mvc;
using PhotoBlogUygulama.Data;
using PhotoBlogUygulama.Models;

namespace PhotoBlogUygulama.Controllers
{
    public class GonderilerController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly IWebHostEnvironment _env;

        public GonderilerController(UygulamaDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public IActionResult Yeni()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Yeni(YeniGonderiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string ext = Path.GetExtension(vm.Resim.FileName);
                string yeniDosyaAd = Guid.NewGuid() + ext;
                string yol = Path.Combine(_env.WebRootPath, "img", "upload", yeniDosyaAd);

                using (var fs = new FileStream(yol, FileMode.CreateNew))
                {
                    vm.Resim.CopyTo(fs);
                }

                _db.Gonderiler.Add(new Gonderi
                {
                    Baslik = vm.Baslik,
                    ResimYolu = yeniDosyaAd
                });

                _db.SaveChanges();

                return RedirectToAction("Index", "Home", new { Sonuc = "Eklendi" });
            }

            return View();
        }
    }
}

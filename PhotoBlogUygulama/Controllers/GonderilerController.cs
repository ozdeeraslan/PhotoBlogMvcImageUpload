using FotoBlog.Models;
using Microsoft.AspNetCore.Mvc;
using PhotoBlogUygulama.Data;
using PhotoBlogUygulama.Models;

namespace PhotoBlogUygulama.Controllers
{
    public class GonderilerController : Controller
    {
        private readonly UygulamaDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly object _logger;


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

        public IActionResult Duzenle(int id)
        {
            Gonderi duzenlenecekGonderi = _db.Gonderiler.FirstOrDefault(g => g.Id == id)!;
            if (duzenlenecekGonderi == null)
            {
                return NotFound();
            }

            DuzenleGonderiViewModel duzenleViewModel = new DuzenleGonderiViewModel
            {
                Id = duzenlenecekGonderi.Id,
                Baslik = duzenlenecekGonderi.Baslik,
                ResimYolu = duzenlenecekGonderi.ResimYolu
            };

            return View(duzenleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Duzenle(DuzenleGonderiViewModel duzenleViewModel)
        {
            if (ModelState.IsValid)
            {
                Gonderi duzenlenecekGonderi = _db.Gonderiler.FirstOrDefault(g => g.Id == duzenleViewModel.Id)!;

                if (duzenlenecekGonderi == null)
                {
                    return NotFound();
                }

                duzenlenecekGonderi.Id = duzenleViewModel.Id;
                duzenlenecekGonderi.Baslik = duzenleViewModel.Baslik;

                // yüklenen bir resim var ise;
                if (duzenleViewModel.Resim != null)
                {
                    ResmiKlasordenKaldir(duzenlenecekGonderi);

                    // Yeni resmi ekle
                    string ext = Path.GetExtension(duzenleViewModel.Resim.FileName);
                    string yeniDosyaAdi = Guid.NewGuid() + ext;
                    string yol = Path.Combine(_env.WebRootPath, "img", "upload", yeniDosyaAdi);

                    using (var fs = new FileStream(yol, FileMode.CreateNew))
                    {
                        duzenleViewModel.Resim.CopyTo(fs);
                    }

                    duzenlenecekGonderi.ResimYolu = yeniDosyaAdi;
                }

                _db.SaveChanges();
                return RedirectToAction("Index", "Home", new { Sonuc = "Duzenlendi" });
            }

            return View(duzenleViewModel);
        }

        public IActionResult Sil(int id)
        {
            Gonderi silinecekGonderi = _db.Gonderiler.FirstOrDefault(g => g.Id == id)!;
            if (silinecekGonderi == null)
            {
                return NotFound();
            }

            return View(silinecekGonderi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sil(Gonderi silinecekGonderi)
        {
            if (ModelState.IsValid)
            {
                // Gönderi silinirken resim bir başka gönderi tarafından kullnamıyorsa silinir.
                ResmiKlasordenKaldir(silinecekGonderi);

                _db.Gonderiler.Remove(silinecekGonderi);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home", new { Sonuc = "Silindi" });
            }

            return View(silinecekGonderi);
        }

        public void ResmiKlasordenKaldir(Gonderi gonderi)
        {
            if (gonderi.ResimYolu != null)
            {
                string silincekResimDosyaAdi = gonderi.ResimYolu;
                string dosyaYolu = Path.Combine(_env.WebRootPath, "img", "upload", silincekResimDosyaAdi);


                if (System.IO.File.Exists(dosyaYolu))
                {
                    bool baskabirGonderiTarafindanKullaniliyorMu = _db.Gonderiler.Any(g => g.ResimYolu == gonderi.ResimYolu && g.Id != gonderi.Id);

                    if (!baskabirGonderiTarafindanKullaniliyorMu)
                    {
                        System.IO.File.Delete(dosyaYolu);
                    }
                }
            }
        }
    }
}


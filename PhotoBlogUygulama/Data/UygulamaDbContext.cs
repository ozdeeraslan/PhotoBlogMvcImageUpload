using Microsoft.EntityFrameworkCore;

namespace PhotoBlogUygulama.Data
{
    public class UygulamaDbContext : DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        {

        }

        public DbSet<Gonderi> Gonderiler { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gonderi>().HasData(
                new Gonderi() { Id = 1, Baslik = "Denizin kokusu...", ResimYolu = "deniz.jpg" },
                new Gonderi() { Id = 2, Baslik = "Manzarayi izliyorum gözlerim kapali...", ResimYolu = "manzara.jpg" }
                );
        }


    }
}

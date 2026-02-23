using Microsoft.EntityFrameworkCore;
using Maison_Sucree.Services.ProductAPI.Models;

namespace Maison_Sucree.Services.ProductAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Strawberry Chocolate Egg",
                Price = 12.00,
                Description = "Ou artizanal din ciocolată fină, umplut cu cremă catifelată de ciocolată și decorat cu căpșuni proaspete și trufe crocante. Desert premium perfect pentru ocazii speciale.",
                ImageUrl = "/images/products/strawberry_chocolate_egg.png",
                CategoryName = "Chocolate"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Red Velvet Cream Petits",
                Price = 13.50,
                Description = "Mini prăjituri Red Velvet cu straturi delicate de blat catifelat și cremă fină de vanilie, decorate cu fructe proaspete. Un desert elegant, perfect pentru momente speciale.",
                ImageUrl = "/images/products/red_velvet.png",
                CategoryName = "Cake"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Vanilla Cream Chocolate Petits",
                Price = 7.90,
                Description = "Mini delicii din ciocolată fină, umplute cu cremă catifelată de vanilie și decorate elegant cu ganache de ciocolată. O combinație rafinată între dulceața vaniliei și intensitatea cacao-ului.",
                ImageUrl = "/images/products/vanilla_chocolate.png",
                CategoryName = "Candy"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Caramel Éclair Royale",
                Price = 11.90,
                Description = "Éclair delicat umplut cu cremă fină de caramel și acoperit cu glazură catifelată și crumble crocant. Un desert rafinat inspirat din patiseria franțuzească clasică.",
                ImageUrl = "/images/products/eclair.png",
                CategoryName = "Cake"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 5,
                Name = "Strawberry Vanilla Petit Squares",
                Price = 10.50,
                Description = "Mini prăjituri rafinate cu straturi delicate de mousse de căpșuni și cremă fină de vanilie, decorate elegant pentru un aspect premium.",
                ImageUrl = "/images/products/strawberry_vanilla_squares.png",
                CategoryName = "Cake"
            });

        }
    }
}

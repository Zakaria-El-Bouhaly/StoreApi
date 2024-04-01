using BCrypt.Net;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;


namespace Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        // seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryId });
            });

            modelBuilder.Entity<Product>().HasMany(p =>
            p.Categories).WithMany(c => c.Products).UsingEntity<ProductCategory>();

            modelBuilder.Entity<Category>().HasMany(c =>
            c.Products).WithMany(p => p.Categories).UsingEntity<ProductCategory>();



            modelBuilder.Entity<Category>().HasData(
                               new Category { Id = 1, Name = "Electronics" },
                               new Category { Id = 2, Name = "Clothes" },
                               new Category { Id = 3, Name = "Books" },
                               new Category { Id = 4, Name = "Devices" }
                               );


            modelBuilder.Entity<Product>().HasData(
                               new Product
                               {
                                   Id = 1,
                                   Name = "Laptop",
                                   Price = 1000,
                                   Quantity = 10,
                                   Description = "Dell XPS 13"
                               },
                               new Product
                               {
                                   Id = 2,
                                   Name = "T-shirt",
                                   Price = 20,
                                   Quantity = 100,
                                   Description = "Blue T-shirt"
                               },
                               new Product
                               {

                                   Id = 3,
                                   Name = "ASP.NET Core",
                                   Price = 10,
                                   Quantity = 50,
                                   Description = "Book about ASP.NET Core"

                               },
                               new Product
                               {
                                   Id = 4,
                                   Name = "Smartphone",
                                   Price = 500,
                                   Quantity = 20,
                                   Description = "Samsung"
                               }
                               );




            modelBuilder.Entity<ProductCategory>().HasData(
               new ProductCategory { ProductId = 1, CategoryId = 1 },
               new ProductCategory { ProductId = 2, CategoryId = 2 },
               new ProductCategory { ProductId = 3, CategoryId = 3 },
               new ProductCategory { ProductId = 4, CategoryId = 4 });


            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "User", NormalizedName = "USER" }
                );

            base.OnModelCreating(modelBuilder);

        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
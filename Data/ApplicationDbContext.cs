using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpicyMealRestaurant.Models;
using System.Reflection.Emit;

namespace SpicyMealRestaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product>Products { get; set; }
        public DbSet<Category>Categories { get; set; } 
        public DbSet<Order>Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient>ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductIngredient>()
                .HasKey(x => new {x.ProductId, x.IngredientId});

            builder.Entity<ProductIngredient>()
                .HasOne(x => x.Product)
                .WithMany(x => x.ProductIngredients)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<ProductIngredient>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.ProductIngredients)
                .HasForeignKey(x => x.IngredientId);

            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizer" },
                new Category { CategoryId = 2, Name = "Name 2" },
                new Category { CategoryId = 3, Name = "Name 3" }
            );

            builder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    IngredientId = 1,
                    Name = "Tomato"
                },
                new Ingredient
                {
                    IngredientId = 2,
                    Name = "Lettuce"
                },
                new Ingredient
                {
                    IngredientId = 3,
                    Name = "Chicken"
                },
                new Ingredient
                {
                    IngredientId = 4,
                    Name = "Cheese"
                },
                new Ingredient
                {
                    IngredientId = 5,
                    Name = "Avocado"
                }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Spicy Chicken Burger",
                    Description = "A spicy chicken burger with fresh ingredients",
                    Price = 5.99m,
                    Stock = 50,
                    CategoryId = 1
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Vegan Burrito",
                    Description = "A tasty burrito with vegan ingredients",
                    Price = 7.49m,
                    Stock = 30,
                    CategoryId = 2
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Pasta Alfredo",
                    Description = "Creamy pasta with Alfredo sauce",
                    Price = 8.99m,
                    Stock = 20,
                    CategoryId = 3
                }
            );

            builder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 1 }, // Spicy Chicken Burger - Tomato
                new ProductIngredient { ProductId = 1, IngredientId = 2 }, // Spicy Chicken Burger - Lettuce
                new ProductIngredient { ProductId = 1, IngredientId = 3 }, // Spicy Chicken Burger - Chicken
                new ProductIngredient { ProductId = 2, IngredientId = 1 }, // Vegan Burrito - Tomato
                new ProductIngredient { ProductId = 2, IngredientId = 5 }, // Vegan Burrito - Avocado
                new ProductIngredient { ProductId = 3, IngredientId = 4 }  // Pasta Alfredo - Cheese
            );


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Faker;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ConsimpleMiddleNetAssignment.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One client to many orders
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var clients = new List<Client>();
            for (int i = 0; i < 25; i++)
            {
                clients.Add(new Client
                {
                    Id = i + 1,
                    Name = Faker.Name.First(),
                    Surname = Faker.Name.Last(),
                    Patronymic = Faker.Name.First(),
                    Birthday = Faker.Identification.DateOfBirth()
                });
            }

            var products = new List<Product>();
            for (int i = 0; i < 50; i++)
            {
                products.Add(new Product
                {
                    Id = i + 1,
                    CategoryId = Faker.RandomNumber.Next(1, 5),
                    VendorCode = Faker.Finance.Isin(),
                    Name = $"Product {i + 1}",
                    Price = new decimal(Faker.RandomNumber.Next(3, 999) + 0.99)
                });
            }

            var allOrderProducts = new List<OrderProduct>();
            var orders = new List<Order>();
            for (int i = 0; i < 200; i++)
            {
                var orderProducts = new List<OrderProduct>();
                for (int j = 0; j < RandomNumber.Next(1, 5); j++)
                {
                    var productIndex = Faker.RandomNumber.Next(49);
                    var quantity = Faker.RandomNumber.Next(1, 10);
                    orderProducts.Add(new OrderProduct
                    {
                        Id = allOrderProducts.Count() + orderProducts.Count() + 1,
                        OrderId = i + 1,
                        ProductId = productIndex + 1,
                        Quantity = quantity,
                        PurchasePrice = products[productIndex].Price * (decimal) quantity
                    });
                }

                orders.Add(new Order
                {
                    Id = i + 1,
                    ClientId = Faker.RandomNumber.Next(1, 25),
                    Code = Faker.Finance.Isin(),
                    OrderDate = DateTime.Today.AddDays(-1 * Faker.RandomNumber.Next(1, 60)),
                    Amount = orderProducts.Sum(m => m.PurchasePrice)
                });
                allOrderProducts.AddRange(orderProducts);
            }

            modelBuilder.Entity<Client>()
                .HasData(clients);

            modelBuilder.Entity<Category>()
                .HasData(new Category {Id = 1, Name = "Category 1"},
                    new Category {Id = 2, Name = "Category 2"},
                    new Category {Id = 3, Name = "Category 4"},
                    new Category {Id = 4, Name = "Category 3"},
                    new Category {Id = 5, Name = "Category 5"});

            modelBuilder.Entity<Product>()
                .HasData(products);

            modelBuilder.Entity<Order>()
                .HasData(orders);

            modelBuilder.Entity<OrderProduct>()
                .HasData(allOrderProducts);
        }
    }
}
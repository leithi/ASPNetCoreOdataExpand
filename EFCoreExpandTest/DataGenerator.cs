using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreExpandTest.DomainModels;
using EFCoreExpandTest.DTOModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreExpandTest
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TestContext(
                serviceProvider.GetRequiredService<DbContextOptions<TestContext>>()))
            {
                // Look for any board games.
                if (context.Products.Any())
                {
                    return;   // Data was already seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        Id = "product1",
                        Title = "titelp1"
                    },
                    new Product
                    {
                        Id = "product2",
                        Title = "titelp2"
                    },
                    new Product
                    {
                        Id = "product3",
                        Title = "titelp3"
                    });

                context.Vendors.AddRange(new Vendor
                {
                    Id = "vendor1",
                    Name = "testvendor1"
                }, new Vendor
                {
                    Id = "vendor2",
                    Name = "testvendor2"
                });

                context.ProductPrices.AddRange(new ProductPrice
                {
                    Price = (decimal)10.99,
                    ProductId = "product1",
                    VendorId = "vendor1"
                },
                    new ProductPrice
                    {
                        Price = (decimal)12.99,
                        ProductId = "product1",
                        VendorId = "vendor2"
                    });

                context.SaveChanges();
            }
        }
    }
}

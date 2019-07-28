using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreExpandTest.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace EFCoreExpandTest.DTOModels
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");
            
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "d02e9e9d");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductPrice>(entity =>
            {
                entity.HasKey(e => new { e.VendorId, e.ProductId });

                entity.ToTable("product_prices", "d02e9e9d");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_vendor_products_products1_idx");

                entity.HasIndex(e => e.VendorId)
                    .HasName("fk_vendor_products_vendors1_idx");

                entity.Property(e => e.VendorId)
                    .HasColumnName("vendor_id")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductPrices)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vendor_products_products1");

                entity.HasOne(d => d.Vendor)
                    .WithMany(p => p.ProductPrices)
                    .HasForeignKey(d => d.VendorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vendor_products_vendors1");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable("vendors", "d02e9e9d");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });
        }
    }
}

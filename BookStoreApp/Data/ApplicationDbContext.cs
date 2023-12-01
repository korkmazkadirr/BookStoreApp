using BookStoreApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BookStoreApp.Data
{
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.BookName)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.CategoryName)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.SubCategoryName)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.SubCategoryId);
            modelBuilder.Entity<BookCategory>()
            .Property(b => b.CategoryId)
            .IsRequired(false);

            modelBuilder.Entity<BookCategory>()
                .Property(b => b.SubCategoryId)
                .IsRequired(false);
        }
    }

}


﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAppWebAPI.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<BookChapter> Chapters { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityTypeBuilder<BookChapter> chapter = modelBuilder.Entity<BookChapter>();
            chapter.ToTable("Chapters").HasKey(p => p.Id);
            chapter.Property<Guid>(p => p.Id)
                .HasColumnType("UniqueIdentifier")
                .HasDefaultValueSql("newid()");
            chapter.Property<string>(p => p.Title).HasMaxLength(120);

        }
    }
}

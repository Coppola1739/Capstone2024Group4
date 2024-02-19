using Azure;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Models;

namespace WebApp.Server.Data
{
    public class CapstoneDbContext : DbContext
    {
        public CapstoneDbContext(DbContextOptions<CapstoneDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Source> Source { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<NoteTags> NoteTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User table
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username) // Ensure username is unique
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            // Source table
            modelBuilder.Entity<Source>()
                .HasKey(s => s.SourceId);
            modelBuilder.Entity<Source>()
                .Property(s => s.SourceId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Source>()
                .Property(s => s.SourceName)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<Source>()
                .Property(s => s.UploadDate)
                .IsRequired();
            modelBuilder.Entity<Source>()
                .Property(s => s.Content)
                .IsRequired();
            modelBuilder.Entity<Source>()
                .Property(s => s.SourceType)
                .IsRequired();
            modelBuilder.Entity<Source>()
                .Property(s => s.AuthorFirstName)
                .HasMaxLength(255);
            modelBuilder.Entity<Source>()
                .Property(s => s.AuthorLastName)
                .HasMaxLength(255);
            modelBuilder.Entity<Source>()
                .Property(s => s.Title)
                .HasMaxLength(255);

            // Configure relationship with User
            modelBuilder.Entity<Source>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sources)
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            // Notes table
            modelBuilder.Entity<Notes>()
                .HasKey(n => n.NotesId);
            modelBuilder.Entity<Notes>()
                .Property(n => n.NotesId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Notes>()
                .Property(n => n.Content)
                .IsRequired();
            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Source)
                .WithMany(s => s.Notes)
                .HasForeignKey(n => n.SourceId)
                .IsRequired();

            // NoteTags table (junction table between Note and Tag)
            modelBuilder.Entity<NoteTags>()
                .HasKey(nt => new { nt.TagName, nt.NotesId });

            // Tags table
            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagName);
        }
    }
}

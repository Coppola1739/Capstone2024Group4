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
        public DbSet<UserFile> UserFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure properties of the User entity
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(255);

            // Configure properties of the UserFile entity
            modelBuilder.Entity<UserFile>()
                .HasKey(uf => uf.UserFileId);
            modelBuilder.Entity<UserFile>()
                .Property(uf => uf.UserFileId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserFile>()
                .Property(uf => uf.FileName)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<UserFile>()
                .Property(uf => uf.UploadDate)
                .IsRequired();
            modelBuilder.Entity<UserFile>()
                .Property(uf => uf.FileContent) // New property for PDF content
                .IsRequired();

            // Configure relationships
            modelBuilder.Entity<UserFile>()
                .HasOne(uf => uf.User)
                .WithMany(u => u.UserFiles)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust the delete behavior based on your requirements
        }
    }
}
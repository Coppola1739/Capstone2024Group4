using Azure;
using Microsoft.EntityFrameworkCore;
using WebApp.Server.Models;

/// <summary>
/// 
/// </summary>
namespace WebApp.Server.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class CapstoneDbContext : DbContext
    {
        public CapstoneDbContext(DbContextOptions<CapstoneDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public DbSet<Source> Source { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public DbSet<Notes> Notes { get; set; }

        /// <summary>
        /// Gets or sets the note tags.
        /// </summary>
        /// <value>
        /// The note tags.
        /// </value>
        public DbSet<NoteTags> NoteTags { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public DbSet<Tag> Tags { get; set; }


        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run. However, it will still run when creating a compiled model.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
        /// examples.
        /// </para>
        /// </remarks>
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
                .HasIndex(u => u.Username)
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

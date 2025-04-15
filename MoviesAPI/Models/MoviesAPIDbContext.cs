using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class MoviesAPIDbContext : DbContext
    {
        public MoviesAPIDbContext(DbContextOptions<MoviesAPIDbContext> options) : base(options)
        {
        }

        //Pass all entities (Models) here
        /* DbSet<Category> represents a collection of all entities in the context or that can be 
        queried from the database. In this case, Categories is a table in the database that corresponds 
        to the Category model. */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email).HasName("PK__Users__A9D10535B2F51717");

                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Password).HasMaxLength(500);
                entity.Property(e => e.Role).HasMaxLength(50);
            });
        }
    }
}

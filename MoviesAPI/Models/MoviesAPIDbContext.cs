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
    }
}

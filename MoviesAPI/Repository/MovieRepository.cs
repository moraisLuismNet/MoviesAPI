using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesAPIDbContext _context;

        public MovieRepository(MoviesAPIDbContext context)
        {
            _context = context;
        }

        // List of Movies sorted by Name
        public ICollection<Movie> GetAllRepository()
        {
            return _context.Movies.OrderBy(c => c.Name).ToList();
        }

        IEnumerable<Movie> IRepository<Movie, int>.GetAllRepository()
        {
            return GetAllRepository();
        }

        // List of Movies by Category
        public ICollection<Movie> GetByCategoryRepository(int categoryId)
        {
            return _context.Movies.Include(ca => ca.Category).Where(ca => ca.categoryId == categoryId).ToList();
        }

        IEnumerable<Movie> IMovieRepository.GetByCategoryRepository(int categoryId)
        {
            return GetByCategoryRepository(categoryId);
        }

        // Search Movie by Name
        public IEnumerable<Movie> SearchByNameRepository(string name)
        {
            IQueryable<Movie> query = _context.Movies;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name) || e.Synopsis.Contains(name));
            }
            return query.ToList();
        }

        // Get Movie by ID
        public Movie GetByIdRepository(int movieId)
        {
            return _context.Movies.FirstOrDefault(c => c.IdMovie == movieId);
        }

        // Check Movie by ID
        public bool ExistsByIdRepository(int movieId)
        {
            return _context.Movies.Any(c => c.IdMovie == movieId);
        }

        // Check Movie By Name
        public bool ExistsByNameRepository(string name)
        {
            bool value = _context.Movies.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        // Create, Update and Delete Movie
        public bool AddRepository(Movie movie)
        {
            movie.CreationDate = DateTime.Now;
            _context.Movies.Add(movie);
            return SaveRepository();
        }

        public bool UpdateRepository(Movie movie)
        {
            movie.CreationDate = DateTime.Now;
            var MovieExistsByIdRepository = _context.Movies.Find(movie.IdMovie);
            if (MovieExistsByIdRepository != null)
            {
                _context.Entry(MovieExistsByIdRepository).CurrentValues.SetValues(movie);
            }
            else
            {
                _context.Movies.Update(movie);
            }

            return SaveRepository();
        }

        public bool DeleteRepository(Movie movie)
        {
            try
            {
                _context.Movies.Remove(movie);
                return SaveRepository();  // Asume que SaveRepository devuelve bool
            }
            catch
            {
                return false;
            }
        }

        // Save changes
        public bool SaveRepository()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    }
}

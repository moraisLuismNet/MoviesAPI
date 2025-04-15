using MoviesAPI.Models;

namespace MoviesAPI.Repository
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
        // List of Movies by Category
        IEnumerable<Movie> GetByCategoryRepository(int categoryId);

        // Search Movie by Name
        IEnumerable<Movie> SearchByNameRepository(string name);

        // Check Movie By Name
        bool ExistsByNameRepository(string name);
    }
}

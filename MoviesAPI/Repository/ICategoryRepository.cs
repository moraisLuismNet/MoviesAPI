using MoviesAPI.Models;

namespace MoviesAPI.Repository
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        // Check Category by Name
        bool ExistsByNameRepository(string name);
    }
}

using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public interface IMovieService : IService<MovieDTO, MovieCreateDTO, int>
    {
        Task<IEnumerable<MovieDTO>> GetByCategoryAsyncService(int categoryId);
        Task<IEnumerable<MovieDTO>> SearchByNameAsyncService(string name);
        Task<bool> ExistsByNameAsyncService(string name);
    }
}

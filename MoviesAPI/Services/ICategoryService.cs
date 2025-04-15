using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public interface ICategoryService : IService<CategoryDTO, CategoryCreateDTO, int>
    {
        Task<bool> ExistsByNameAsyncService(string name);
    }
}

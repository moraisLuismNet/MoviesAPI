using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public interface ICategoryService : IService<CategoryDTO, CategoryCreateDTO, CategoryUpdateDTO, int>
    {
        Task<bool> ExistsByNameAsyncService(string name);
    }
}

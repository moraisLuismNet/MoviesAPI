using MoviesAPI.DTOs;

namespace MoviesAPI.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAllCategoriesService();
        CategoryDTO GetCategoryByIdService(int id);
        CategoryDTO CreateCategoryService(CategoryCreateDTO categoryCreateDTO);
        bool UpdateCategoryService(CategoryDTO categoryDTO);
        bool DeleteCategoryService(int id);
        bool CategoryExistsByNameService(string name);
        bool CategoryExistsByIdService(int id);
    }
}

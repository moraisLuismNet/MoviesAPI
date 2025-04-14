using MoviesAPI.Models;

namespace MoviesAPI.Repository
{
    public interface ICategoryRepository
    {
        // List of Categories
        ICollection<Category> GetCategoriesRepository();

        // Category by ID
        Category GetCategoryByIdRepository(int CategoryId);

        // Check Category by ID
        bool CategoryExistsByIdRepository(int CategoryId);

        // Check Category by Name
        bool CategoryExistsByNameRepository(string name);
        
        // Create Category
        bool CreateCategoryRepository(Category category);
        
        // Update Category
        bool UpdateCategoryRepository(Category category);
        
        // Delete Category
        bool DeleteCategoryRepository(Category category);
        
        // Save changes
        bool SaveRepository();
    }
}

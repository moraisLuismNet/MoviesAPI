using MoviesAPI.Models;

namespace MoviesAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly MoviesAPIDbContext _context;

        public CategoryRepository(MoviesAPIDbContext context)
        { 
            _context = context;
        }

        IEnumerable<Category> IRepository<Category, int>.GetAllRepository()
        {
            return GetAllRepository();
        }

        // List of Categories sorted by Name
        public ICollection<Category> GetAllRepository()
        {
            return _context.Categories.OrderBy(c => c.Name).ToList();
        }

        // Category by ID
        public Category GetByIdRepository(int CategoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.IdCategory == CategoryId);
        }

        // Check Category by ID
        public bool ExistsByIdRepository(int CategoryId)
        {
            return _context.Categories.Any(c => c.IdCategory == CategoryId);
        }

        // Check Category by Name
        public bool ExistsByNameRepository(string name)
        {
            bool value = _context.Categories.Any(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        // Create Category
        public bool AddRepository(Category category)
        {
            category.CreationDate = DateTime.Now;
            _context.Categories.Add(category);
            return SaveRepository();
        }

        // Update Category
        public bool UpdateRepository(Category category)
        {
            category.CreationDate = DateTime.Now;
            var CategoryExistsById = _context.Categories.Find(category.IdCategory);
            if (CategoryExistsById != null)
            {
                _context.Entry(CategoryExistsById).CurrentValues.SetValues(category);
            }
            else
            {
                _context.Categories.Update(category);
            }

            return SaveRepository();
        }

        // Delete Category
        public bool DeleteRepository(Category category)
        {
            _context.Categories.Remove(category);
            return SaveRepository();
        }

        // Save changes
        public bool SaveRepository()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    }
}

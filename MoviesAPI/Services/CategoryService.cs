using AutoMapper;
using MoviesAPI.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Repository;

namespace MoviesAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsyncService()
        {
            var categories = _categoryRepository.GetAllRepository();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsyncService(int id)
        {
            var category = _categoryRepository.GetByIdRepository(id);
            return _mapper.Map<CategoryDTO>(category);
        }
        
        public async Task<CategoryDTO> CreateAsyncService(CategoryCreateDTO categoryCreateDTO)
        {
            if (await ExistsByNameAsyncService(categoryCreateDTO.Name))
            {
                throw new InvalidOperationException("The category already exists");
            }

            var category = _mapper.Map<Category>(categoryCreateDTO);
            category.CreationDate = DateTime.Now;

            if (!_categoryRepository.AddRepository(category))
            {
                throw new Exception($"Something went wrong while saving the record {category.Name}");

            }

            return _mapper.Map<CategoryDTO>(category);
        }
        
        public async Task UpdateAsyncService(int categoryId,CategoryDTO categoryDTO)
        {
            if (categoryId != categoryDTO.IdCategory)
            {
                throw new ArgumentException("ID mismatch");
            }

            var existingCategory = _categoryRepository.GetByIdRepository(categoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");
            }

            var category = _mapper.Map<Category>(categoryDTO);
            category.CreationDate = DateTime.Now;

            if (!_categoryRepository.UpdateRepository(category))
            {
                throw new Exception($"Something went wrong updating the record {category.Name}");
            }

        }

        public async Task<bool> DeleteAsyncService(int categoryId)
        {
            var category = _categoryRepository.GetByIdRepository(categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {categoryId} not found");
            }

            return _categoryRepository.DeleteRepository(category);
        }

        public async Task<bool> ExistsByNameAsyncService(string name)
        {
            return _categoryRepository.ExistsByNameRepository(name);
        }
        
        public async Task<bool> ExistsByIdAsyncService(int id)
        {
            return _categoryRepository.ExistsByIdRepository(id);
        }
    }
}

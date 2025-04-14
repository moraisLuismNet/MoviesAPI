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

        public IEnumerable<CategoryDTO> GetAllCategoriesService()
        {
            var categories = _categoryRepository.GetCategoriesRepository();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public CategoryDTO GetCategoryByIdService(int id)
        {
            var category = _categoryRepository.GetCategoryByIdRepository(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public CategoryDTO CreateCategoryService(CategoryCreateDTO categoryCreateDTO)
        {
            var category = _mapper.Map<Category>(categoryCreateDTO);
            category.CreationDate = DateTime.Now;

            if (!_categoryRepository.CreateCategoryRepository(category))
            {
                return null;
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public bool UpdateCategoryService(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            return _categoryRepository.UpdateCategoryRepository(category);
        }

        public bool DeleteCategoryService(int id)
        {
            var category = _categoryRepository.GetCategoryByIdRepository(id);
            if (category == null)
            {
                return false;
            }

            return _categoryRepository.DeleteCategoryRepository(category);
        }

        public bool CategoryExistsByNameService(string name)
        {
            return _categoryRepository.CategoryExistsByNameRepository(name);
        }

        public bool CategoryExistsByIdService(int id)
        {
            return _categoryRepository.CategoryExistsByIdRepository(id);
        }
    }
}

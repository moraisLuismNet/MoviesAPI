using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // Get Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var listCategoriesDTO = _categoryService.GetAllCategoriesService();
            return Ok(listCategoriesDTO);
        }

        // Get Category by Id
        [HttpGet("{categoryId:int}", Name = "GetCategoriaById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int categoryId)
        {
            var itemCategoryDTO = _categoryService.GetCategoryByIdService(categoryId);

            if (itemCategoryDTO == null)
            {
                return NotFound();
            }

            return Ok(itemCategoryDTO);
        }

        // Create Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryCreateDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryService.CategoryExistsByNameService(categoryCreateDTO.Name))
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(404, ModelState);
            }

            var categoryDTO = _categoryService.CreateCategoryService(categoryCreateDTO);

            if (categoryDTO == null)
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {categoryCreateDTO.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoriaById", new { categoryId = categoryDTO.IdCategory }, categoryDTO);
        }

        // Update category
        [HttpPut("{categoryId:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryDTO == null || categoryId != categoryDTO.IdCategory)
            {
                return BadRequest(ModelState);
            }

            if (!_categoryService.CategoryExistsByIdService(categoryId))
            {
                return NotFound($"Category with ID not found {categoryId}");
            }

            if (!_categoryService.UpdateCategoryService(categoryDTO))
            {
                ModelState.AddModelError("", $"Something went wrong updating the registry {categoryDTO.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // Delete category
        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryService.CategoryExistsByIdService(categoryId))
            {
                return NotFound();
            }

            if (!_categoryService.DeleteCategoryService(categoryId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record with id {categoryId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

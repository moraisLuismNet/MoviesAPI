using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Get Categories
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var listCategoriesDTO = await _categoryService.GetAllAsyncService();
            return Ok(listCategoriesDTO);
        }

        // Get Category by Id
        [AllowAnonymous]
        [HttpGet("{categoryId:int}", Name = "GetCategoriaById")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var itemCategoryDTO = await _categoryService.GetByIdAsyncService(categoryId);

            if (itemCategoryDTO == null)
            {
                return NotFound();
            }

            return Ok(itemCategoryDTO);
        }

        // CreateCategory
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryCreateDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(categoryCreateDTO.Name) || await _categoryService.ExistsByNameAsyncService(categoryCreateDTO.Name))
            {
                ModelState.AddModelError("", "Category already exists or name is null");
                return StatusCode(404, ModelState);
            }

            var categoryDTO = await _categoryService.CreateAsyncService(categoryCreateDTO);

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
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryDTO == null || categoryId != categoryDTO.IdCategory)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.UpdateAsyncService(categoryId, categoryDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Something went wrong updating the registry: {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        // Delete category
        [HttpDelete("{categoryId:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!await _categoryService.ExistsByIdAsyncService(categoryId))
            {
                return NotFound($"Category with ID {categoryId} not found.");
            }

            if (!await _categoryService.DeleteAsyncService(categoryId))
            {
                ModelState.AddModelError("", $"Something went wrong deleting the record with id {categoryId}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NK.Core.Business.Model.Category;
using NK.Core.DataAccess.Repository;
using NK.Core.Model.Entities;

namespace NK.Core.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return await Task.FromResult(BadRequest("Id không được phép để trống!"));
            }

            var categories = await _categoryRepository.GetByIdAsync(Id);
            if(categories == null)
            {
                return await Task.FromResult(NotFound());
            }
            return Ok(categories); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                if(categoryDto == null)
                {
                    return BadRequest("Category object is null");
                }

                var existingCategory = await _categoryRepository.GetCategoryByNameAsync(categoryDto.Name);
                if(existingCategory != null)
                {
                    return Conflict("Category with the same Name already exists");
                }

                Category category = new()
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description
                };

                await _categoryRepository.AddAsync(category);
                return Ok(category);
            }
            catch
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, CategoryDto categoryDto)
        {
            try
            {
                if(categoryDto == null)
                {
                    return BadRequest("Category object is null");
                }

                var categoryItem = await _categoryRepository.GetByIdAsync(id);
                if(categoryItem == null)
                {
                    return NotFound("Category not found");
                }

                var existingCategory = await _categoryRepository.GetCategoryByNameAsync(categoryDto.Name);
                if( existingCategory != null && existingCategory != categoryItem)
                {
                    return Conflict("Category with the same Name already exists");
                }

                categoryItem.Name = categoryDto.Name;
                categoryItem.Description = categoryDto.Description;

                await _categoryRepository.UpdateAsync(id, categoryItem);
                return NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteCategory(string id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if(category == null)
                {
                    return false;
                }

                await _categoryRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

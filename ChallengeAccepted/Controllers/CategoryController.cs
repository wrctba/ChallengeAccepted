using ChallengeAccepted.DTO;
using ChallengeAccepted.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChallengeAccepted.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        const int DEFAULT_PAGE_SIZE = 20;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;
       
        /// <summary>
        /// Get a paginate list of catagories
        /// </summary>
        /// <param name="page">Set the current page, greater than 1</param>
        /// <param name="pageSize">Set a page size betwee 1(one) and 100(a hundred)</param>
        /// <param name="search">String to be search</param>
        /// <returns>List of categories</returns>
        [HttpGet]
        public async Task<IList<CategoryDto>> ListAsync([FromQuery]string? page = null, [FromQuery]string? pageSize = null, [FromQuery] string? name = null, [FromQuery] string? slug = null)
        {
            if (!int.TryParse(page, out int safePage) || safePage == 0)
            {
                safePage = 1;
            }

            if (!int.TryParse(pageSize, out int sagePageSize) || sagePageSize <= 0 || sagePageSize > 100)
            {
                sagePageSize = DEFAULT_PAGE_SIZE;
            }

            return await _categoryService.ListAsync(safePage, sagePageSize, name, slug);
        }

        /// <summary>
        /// Get a category by id
        /// </summary>
        /// <param name="id">Category unique id</param>
        /// <returns>A category</returns>
        [HttpGet("{id}")]
        public async Task<CategoryDto> GetAsync(string id)
        {
            return await _categoryService.GetAsync(id);
        }

        /// <summary>
        /// Get a catagory tree by id
        /// </summary>
        /// <param name="id">Category unique id</param>
        /// <returns>A catagory tree</returns>
        [HttpGet("{id}/tree")]
        public async Task<CategoryTreeDto> GetTreeAsync(string id)
        {
            return await _categoryService.GetTreeAsync(id);
        }

        /// <summary>
        /// Insert a new catagory
        /// </summary>
        /// <param name="categoryPostDto">Object with catagory properties</param>
        /// <returns>caragory id inserted</returns>
        [HttpPost]
        public async Task<CategoryPostResponseDto> PostAsync(CategoryPostDto categoryPostDto)
        {
            return await _categoryService.CreateAsync(categoryPostDto);
        }

        /// <summary>
        /// Update a category by id
        /// </summary>
        /// <param name="id">Catagory id</param>
        /// <param name="categoryPutDto">Object with propesties to update</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task PutAsync(string id, CategoryPutDto categoryPutDto)
        {
            await _categoryService.UpdateAsync(id, categoryPutDto);
        }

        /// <summary>
        /// Update a category to hide by id
        /// </summary>
        /// <param name="id">Catagory id</param>
        /// <returns>None</returns>
        [HttpPut("{id}/hide")]
        public async Task HideAsync(string id)
        {
            await _categoryService.UpdateVisibilityAsync(id, false);
        }

        /// <summary>
        /// Update a category to visible by id
        /// </summary>
        /// <param name="id">Catagory id</param>
        /// <returns>None</returns>
        [HttpPut("{id}/show")]
        public async Task ShowAsync(string id)
        {
            await _categoryService.UpdateVisibilityAsync(id, true);
        }

        /// <summary>
        /// Delete a category by id
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _categoryService.DeleteAsync(id);
            return;
        }
    }
}

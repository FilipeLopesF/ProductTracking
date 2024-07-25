using _3DTrackingProducts.Api.Core;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace _3DTrackingProducts.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllCategories()
        {
            List<Category> categories = await _unitOfWork.Category.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCategoryById(int id)
        {
            Category? category = await _unitOfWork.Category.GetByIdAsync(id);
            if(category == null)
            {
                return NotFound($"Category with id: {id} was not found");
            }
            return Ok(category);
        }

        /*
        [HttpGet("{name}")]
        public async Task<IActionResult> getCategoryByName(string name)
        {
            Category? category = await _unitOfWork.CategoryRepository.GetByNameAsync(name);
            if (category == null)
            {
                return NotFound($"Category with name : {name} was not found");
            }
            return Ok(category);
        }*/

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryResource categoryResource)
        {
            if (categoryResource.Name.IsNullOrEmpty())
            {
                return BadRequest("Name must not be empty");
            }

            var category = _mapper.Map<CategoryResource,Category>(categoryResource);

            Category? categoryAlreadyExists = await _unitOfWork.Category.GetByNameAsync(category.Name);
            if (categoryAlreadyExists == null)
            {
                if(await _unitOfWork.Category.AddCategoryAsync(category))
                {
                    return CreatedAtAction(nameof(AddCategory), new { name = category.Name }, category);
                }
            }
            return BadRequest($"Category with name {category.Name} already exists");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,CategoryResource categoryResource)
        {
            if (categoryResource.Name.IsNullOrEmpty())
            {
                return BadRequest("Name must not be empty");
            }

            var category = _mapper.Map<CategoryResource, Category>(categoryResource);

            Category? categoryDoesntExist = await _unitOfWork.Category.GetByIdAsync(id);
            if(categoryDoesntExist == null)
            {
                return NotFound($"Cannot update category with id: {id} because no category with such id was found");
            }

            if(!await _unitOfWork.Category.UpdateCategoryAsync(id,category))
            {
                return StatusCode(500, $"There was an error that prevented the service from updating the category with id: {id}");
            }
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            Category? categoryAlreadyExists = await _unitOfWork.Category.GetByIdAsync(id);
            if (categoryAlreadyExists == null)
            {
                return NotFound($"Category with id: {id} doesn't exist");
            }

            if(!await _unitOfWork.Category.DeleteCategoryAsync(id))
            {
                return StatusCode(500, $"There was an error that prevented the service from deleting the category with id: {id}");
            }
            return NoContent();
        }

    }
}

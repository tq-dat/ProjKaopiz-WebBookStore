using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBookStore.Dto;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using WebBookStore.Repository;

namespace WebBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Getcategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            return !ModelState.IsValid ? Ok(categories) : BadRequest(ModelState);
        }

        [HttpGet("{Id}")]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
            return !ModelState.IsValid ? Ok(category) : BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateCategory(int id , string name)
        {
            if (_categoryRepository.GetCategory(id) == null)
                return NotFound();

            if (!_categoryRepository.UpdateCategory(id,name))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (_categoryRepository.GetCategory(id) == null)
                return NotFound();

            if (!_categoryRepository.DeleteCategory(id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}

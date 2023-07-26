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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductController( IProductRepository productRepository,ICategoryRepository categoryRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            return !ModelState.IsValid ? Ok(products) : BadRequest(ModelState);
        }

        [HttpGet("{prodId:int}")]
        public IActionResult GetProduct(int prodId)
        {
            if (!_productRepository.ProductExists(prodId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(prodId));
            return !ModelState.IsValid ? Ok(product) : BadRequest(ModelState);
        }

        [HttpGet("category/{categoryId}")]
        public IActionResult GetProductsByCategoryId(int categoryId)
        {
            var produts = _mapper.Map<List<ProductDto>>(_categoryRepository.GetProductsByCategory(categoryId));
            return !ModelState.IsValid ? Ok(produts) : BadRequest(ModelState);
        }

        [HttpGet("{name}")]
        public IActionResult GetProduct(string name)
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByName(name));
            if (products.Count <= 0) 
                return NotFound();
            return !ModelState.IsValid ? Ok(products) : BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult CreateProduct(int categoryId, ProductDto productCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.Trim().ToUpper())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            var productMap = _mapper.Map<Product>(productCreate);
            if (!_productRepository.CreateProduct(categoryId , productMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateProduct(int prodId, ProductDto product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_productRepository.GetProduct(prodId) == null)
                return NotFound();

            var productMap = _mapper.Map<Product>(product);
            if (!_productRepository.UpdateProduct(prodId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int  prodId)
        {
            if (_productRepository.GetProduct(prodId) == null)
                return NotFound();

            if (!_productRepository.DeleteProduct(prodId))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");
        }
    }
}

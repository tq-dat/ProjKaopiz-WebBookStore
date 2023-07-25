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
        private readonly IMapper _mapper;

        public ProductController( IProductRepository productRepository, IMapper mapper) 
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }
        [HttpGet("{prodId:int}")]
        public IActionResult GetProduct(int prodId)
        {
            if (!_productRepository.ProductExists(prodId))
                return NotFound();

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(prodId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpGet("{prodName}")]
        public IActionResult GetProduct(string prodName)
        {

            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByName(prodName));
            if (products.Count <= 0) 
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(products);

        }

        [HttpPost]
        public IActionResult CreateProduct(int categoryId, ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            if (_productRepository.GetProduct(prodId) == null)
                return NotFound();
            if (product == null)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(product);

            if (!_productRepository.UpdateProduct(prodId, productMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
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

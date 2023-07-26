using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Macs;
using WebBookStore.Dto;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using WebBookStore.Repository;

namespace WebBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemController(ICartItemRepository cartItemRepository, IMapper mapper) 
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _mapper.Map<List<CartItem>>(_cartItemRepository.GetCartItems());
            return !ModelState.IsValid ? Ok(cartItems) : BadRequest(ModelState);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetCartItemByOrderId(int orderId)
        {
            var cartItems = _cartItemRepository.GetCartItemByOrderId(orderId);
            if (cartItems.Count() <= 0)
                return BadRequest();
            return !ModelState.IsValid ? Ok(cartItems) : BadRequest(ModelState);
        }

        [HttpPost]
        public IActionResult CreateCartItem(int productId, int userId, CartItemCreate cartItemCreate)
        {
            if (cartItemCreate == null)
                return BadRequest(ModelState);

            var cartItemMap = _mapper.Map<CartItem>(cartItemCreate);
            if (!_cartItemRepository.CreateCartItem(productId, userId, cartItemMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateCartItem(CartItemCreate cartItemUpdate,int id)
        {
            if(cartItemUpdate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartItemMap = _mapper.Map<CartItem>(cartItemUpdate);
            if (!_cartItemRepository.UpdateCartItem(cartItemMap,id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteCartItem(int id)
        {
            if (!_cartItemRepository.CartItemExists(id))
                return NotFound();

            if (!_cartItemRepository.DeleteCartItem(id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");

        }
    }
}

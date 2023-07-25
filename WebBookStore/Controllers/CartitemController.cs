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
    public class CartitemController : ControllerBase
    {
        private readonly ICartitemRepository _cartitemRepository;
        private readonly IMapper _mapper;

        public CartitemController(ICartitemRepository cartitemRepository, IMapper mapper) 
        {
            _cartitemRepository = cartitemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCartitems()
        {
            var cartitems = _mapper.Map<List<Cartitem>>(_cartitemRepository.GetCartitems());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cartitems);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetCartitemByOrderId(int orderId)
        {
            var cartitems = _cartitemRepository.GetCartitemByOrderId(orderId);
            if (cartitems.Count() <= 0)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cartitems);
        }

        [HttpPost]
        public IActionResult CreateCartitem(int productId, int userId, CartitemCreate cartitemCreate)
        {
            if (cartitemCreate == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartitemMap = _mapper.Map<Cartitem>(cartitemCreate);

            if (!_cartitemRepository.CreateCartitem(productId, userId, cartitemMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateCartitem(CartitemCreate cartitemUpdate,int id)
        {
            if(cartitemUpdate == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartitemMap = _mapper.Map<Cartitem>(cartitemUpdate);
            if (!_cartitemRepository.UpdateCartitem(cartitemMap,id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteCartitem(int id)
        {
            if (!_cartitemRepository.CartitemExists(id))
            {
                return NotFound();
            }

            if (!_cartitemRepository.DeleteCartitem(id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");

        }
    }
}

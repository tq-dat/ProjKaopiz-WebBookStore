using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using WebBookStore.Dto;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using WebBookStore.Repository;

namespace WebBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<Order>>(_orderRepository.GetOrders());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(orders);
        }

        [HttpGet("{status}")]
        public IActionResult GetOrderByStatus(string status)
        {
            var orders = _mapper.Map<List<Order>>(_orderRepository.GetOrderByStatus(status));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetOrder(int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();
            var order = _mapper.Map<Order>(_orderRepository.GetOrder(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderCreate orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cartitemIds = orderCreate.cartitemIds;
            var orderMap = _mapper.Map<Order>(orderCreate.orderDto);

            if (!_orderRepository.CreateOrder(cartitemIds, orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult UpdateOrder(int orderId, string status, int manageId)
        {
            if (!_orderRepository.UpdateOrder(orderId, status, manageId))
            {
                return BadRequest();
            }

            return Ok("Successfully updated");
        }
        
    }
}

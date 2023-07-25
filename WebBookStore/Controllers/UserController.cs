using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebBookStore.Dto;
using WebBookStore.Interfaces;
using WebBookStore.Models;
using WebBookStore.Repository;

namespace WebBookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsersByRole(role));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(users);
        }

        [HttpGet("{userId:int}")]
        public IActionResult GetUserByUserId(int userId) 
        {
            if (!_userRepository.UserExists(userId)) 
                return NotFound();
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("seachUser/{name}")]
        public IActionResult GetUsersByName(string name)
        {
            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUsersByName(name));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }

        [HttpGet("cartitem/{userId}")]
        public IActionResult GetCartitemByUserId(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var cartitems = _mapper.Map<List<CartitemDto>>(_userRepository.GetCartitemByUserId(userId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cartitems);
        }

        [HttpGet("order/{userId}")]
        public IActionResult GetOrderByUserId(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var orders = _mapper.Map<List<OrderDto>>(_userRepository.GetOrdersByUserId(userId));
            if (orders.Count <= 0)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(orders);
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest(ModelState);
            if (!_userRepository.UserExists(userLogin))
                return NotFound(ModelState);
            return Ok("Login success");
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(c => c.UserName.Trim().ToUpper() == userCreate.UserName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        public IActionResult updateUser(UserDto userUpdate) 
        {
            if (userUpdate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userUpdate);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully updated");
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            if (!_userRepository.DeleteUser(id))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully deleted");

        }
    }
}

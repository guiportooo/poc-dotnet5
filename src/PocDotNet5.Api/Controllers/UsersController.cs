using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocDotNet5.Api.Domain.Entities;
using PocDotNet5.Api.Domain.Repositories;
using PocDotNet5.Api.Models.Requests;
using PocDotNet5.Api.Models.Responses;

namespace PocDotNet5.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet]
        [Route("{id:int}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.FindAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrors))]
        public async Task<IActionResult> Post([FromBody] CreateUser createUser)
        {
            var user = new User(createUser.FirstName, createUser.LastName, createUser.Email, createUser.DateOfBirth);

            await _userRepository.AddAsync(user);
            
            return CreatedAtRoute("Get", new {id = user.Id}, new UserCreated(user.FirstName,
                user.LastName,
                user.Email,
                user.DateOfBirth,
                user.Active));
        }
    }
}
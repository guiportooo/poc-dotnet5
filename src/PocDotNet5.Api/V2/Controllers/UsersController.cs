namespace PocDotNet5.Api.V2.Controllers
{
    using System.Threading.Tasks;
    using Api.Models.Responses;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Requests;
    using Models.Responses;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
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

            return Ok(new UserCreated(user.FullName,
                user.Email,
                user.DateOfBirth));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrors))]
        public async Task<IActionResult> Post([FromBody] CreateUser createUser)
        {
            var user = new User(createUser.FirstName, createUser.LastName, createUser.Email, createUser.DateOfBirth);

            await _userRepository.AddAsync(user);

            return CreatedAtRoute("Get", new {id = user.Id}, new UserCreated(user.FullName,
                user.Email,
                user.DateOfBirth));
        }
    }
}
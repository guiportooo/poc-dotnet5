namespace PocDotNet5.Api.V1.Controllers
{
    using System.Threading.Tasks;
    using Api.Models.Responses;
    using AutoMapper;
    using Domain.Entities;
    using Domain.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Requests;
    using Models.Responses;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UsersController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("{id:int}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.FindAsync(id);

            if (user == null)
                return NotFound();

            var userCreated = _mapper.Map<UserCreated>(user);
            return Ok(userCreated);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrors))]
        public async Task<IActionResult> Post([FromBody] CreateUser createUser)
        {
            var user = _mapper.Map<User>(createUser);
            await _userRepository.AddAsync(user);
            var userCreated = _mapper.Map<UserCreated>(user);
            return CreatedAtRoute("Get", new {id = user.Id}, userCreated);
        }
    }
}
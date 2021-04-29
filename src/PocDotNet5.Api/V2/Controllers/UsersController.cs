namespace PocDotNet5.Api.V2.Controllers
{
    using System.Threading.Tasks;
    using Api.Schemas.Responses;
    using AutoMapper;
    using Domain.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Schemas.Requests;
    using Schemas.Responses;

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsersController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id:int}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetUser(id);
            var user = await _mediator.Send(query);

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
            var command = _mapper.Map<Domain.Commands.CreateUser>(createUser);
            var id = await _mediator.Send(command);
            return CreatedAtRoute("Get", new {id});
        }
    }
}
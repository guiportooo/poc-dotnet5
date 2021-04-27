using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocDotNet5.Api.Models.Requests;
using PocDotNet5.Api.Models.Responses;

namespace PocDotNet5.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserCreated))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ValidationErrors))]
        public IActionResult Index([FromBody] CreateUser createUser) => 
            Ok(new UserCreated(createUser.FirstName,
                createUser.LastName, 
                createUser.Email, 
                createUser.DateOfBirth, 
                true));
    }
}
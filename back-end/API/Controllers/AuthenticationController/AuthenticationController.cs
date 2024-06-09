using Application.Commands.Users.Register;
using Application.Dtos;
using Application.Validators.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;

namespace API.Controllers.AuthenticationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        internal readonly IMediator _mediator;
        internal readonly UserValidator _userValidator;

        public AuthenticationController(IMediator mediator,
                               UserValidator userValidator)
        {
            _mediator = mediator;
            _userValidator = userValidator;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Errors), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserDto userToRegister)
        {
            var inputValidation = _userValidator.Validate(userToRegister);

            if (!inputValidation.IsValid)
            {
                return BadRequest(inputValidation.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new RegisterUserCommand(userToRegister)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

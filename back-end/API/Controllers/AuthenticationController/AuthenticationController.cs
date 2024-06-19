using Application.Commands.Users.Register;
using Application.Dtos;
using Application.Dtos.Users;
using Application.Queries.Users.Login;
using Application.Validators.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            try
            {
                var inputValidation = _userValidator.Validate(command.NewUser);

                if (!inputValidation.IsValid)
                {
                    return BadRequest(inputValidation.Errors.ConvertAll(errors => errors.ErrorMessage));
                }

                var result = await _mediator.Send(command);
                return Ok("User registered successfully.");

            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] UserDto userToLogin)
        {
            var inputValidation = _userValidator.Validate(userToLogin);

            if (!inputValidation.IsValid)
            {
                return BadRequest(inputValidation.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                string token = await _mediator.Send(new LoginUserQuery(userToLogin));

                return Ok(new TokenDto { TokenValue = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

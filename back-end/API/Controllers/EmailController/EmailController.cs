using Application.Commands.Services.Emails.BookingConfirmation;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmailController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        internal readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Booking confirmation email
        [HttpPost]
        [Route("bookingConfirmationEmail"), Authorize]
        public async Task<IActionResult> BookingConfirmationEmail([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                var query = new BookingConfirmationEmailCommand(appointmentDto, userName);
                var result = await _mediator.Send(query);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

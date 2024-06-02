using Application.Queries.Appointments.GetAllAppointments;
using Domain.Models.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AppointmentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        internal readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get all Appointments
        [HttpGet]
        [Route("getAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var query = new GetAllAppointmentsQuery();
                var result = await _mediator.Send(query);

                if (!(result is not List<Appointment> appointment || appointment.Count == 0))
                {
                    return Ok(appointment);
                }
                else { return Ok(); }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Application.Dtos;
using Application.Queries.Appointments.GetAllAppointments;
using Application.Queries.Appointments.GetAppointmentById;
using Domain.Models.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Appointments.AddNewAppoinment;
using Application.Commands.Appointments.UpdateAppointment;

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

        //Get an appointment by id
        [HttpGet]
        [Route("getAppointmentById/{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(Guid appointmentId)
        {
            try
            {
                Appointment appointment = await _mediator.Send(new GetAppointmentByIdQuery(appointmentId));
                if (appointment == null)
                {
                    ModelState.AddModelError("AppointmentNotFound", $"This appointment Id {appointmentId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        // Create new appointment
        [HttpPost]
        [Route("addNewAppointment")]
        public async Task<IActionResult> AddNewAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new AddNewAppointmentCommand(appointmentDto);
                var result = await _mediator.Send(command);

                return Ok(result); // Return successful result
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Handle unexpected exceptions
            }
        }

        // update appointment
        [HttpPost]
        [Route("updateAppointment{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto, Guid appointmentId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var command = new UpdateAppointmentCommand(appointmentDto, appointmentId);
                var result = await _mediator.Send(command);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}

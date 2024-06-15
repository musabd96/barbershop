using Application.Dtos;
using Application.Queries.Appointments.GetAllAppointments;
using Application.Queries.Appointments.GetAppointmentById;
using Domain.Models.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.Appointments.AddNewAppoinment;
using Application.Commands.Appointments.UpdateAppointment;
using Application.Commands.Appointments.DeleteAppointment;
using FluentValidation.Results;
using Application.Validators.Appointmnet;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.AppointmentController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        internal readonly IMediator _mediator;
        internal readonly AppointmentValidator _validator;

        public AppointmentController(IMediator mediator, AppointmentValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        //Get all Appointments
        [HttpGet]
        [Route("getAllAppointments"), Authorize]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                var query = new GetAllAppointmentsQuery(userName);
                var result = await _mediator.Send(query);

                if (result is List<Appointment> appointments && appointments.Count > 0)
                {
                    return Ok(appointments);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Get an appointment by id
        [HttpGet]
        [Route("getAppointmentById/{appointmentId}"), Authorize]
        public async Task<IActionResult> GetAppointmentById(Guid appointmentId)
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                Appointment appointment = await _mediator.Send(new GetAppointmentByIdQuery(appointmentId, userName));
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

        [HttpPost]
        [Route("addNewAppointment"), Authorize]
        public async Task<IActionResult> AddNewAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                var validationResult = _validator.Validate(appointmentDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new AddNewAppointmentCommand(appointmentDto, userName);
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
        [Route("updateAppointment/{appointmentId}"), Authorize]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto, Guid appointmentId)
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                ValidationResult validationResult = await _validator.ValidateAsync(appointmentDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new UpdateAppointmentCommand(appointmentDto, appointmentId, userName);
                var result = await _mediator.Send(command);
                if (result == null)
                {
                    ModelState.AddModelError("AppointmentNotFound", $"This appointment Id {appointmentId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // delete appointment
        [HttpDelete]
        [Route("deleteAppointment/{appointmentId}"), Authorize]
        public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
        {
            try
            {
                // Get the userName of the authenticated user
                string userName = HttpContext.User.Identity!.Name!;

                Appointment appointment = await _mediator.Send(new DeleteAppointmentCommand(appointmentId, userName));
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
    }
}

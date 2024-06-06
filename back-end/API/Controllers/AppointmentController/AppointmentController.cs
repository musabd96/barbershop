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
        [Route("getAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var query = new GetAllAppointmentsQuery();
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

        [HttpPost]
        [Route("addNewAppointment")]
        public async Task<IActionResult> AddNewAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                var validationResult = _validator.Validate(appointmentDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
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
        [Route("updateAppointment/{appointmentId}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto, Guid appointmentId)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(appointmentDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new UpdateAppointmentCommand(appointmentDto, appointmentId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // delete appointment
        [HttpDelete]
        [Route("deleteAppointment/{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
        {
            try
            {
                Appointment appointment = await _mediator.Send(new DeleteAppointmentCommand(appointmentId));
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

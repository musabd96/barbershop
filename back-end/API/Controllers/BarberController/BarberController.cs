using Application.Commands.Barbers.AddNewBarber;
using Application.Commands.Barbers.DeleteBarber;
using Application.Commands.Barbers.UpdateBarber;
using Application.Dtos;
using Application.Queries.Barbers.GetAllBarbers;
using Application.Queries.Barbers.GetBarberById;
using Application.Validators.Barber;
using Domain.Models.Barbers;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BarberController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : Controller
    {
        internal readonly IMediator _mediator;
        internal readonly BarberValidator _validator;

        public BarberController(IMediator mediator, BarberValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        //Get all barbers
        [HttpGet]
        [Route("getAllBarbers")]
        public async Task<IActionResult> GetAllBarbers()
        {
            try
            {
                var query = new GetAllBarbersQuery();
                var result = await _mediator.Send(query);

                if (result is List<Barber> appointments && appointments.Count > 0)
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

        //Get a barber by id
        [HttpGet]
        [Route("getBarberById/{barberId}")]
        public async Task<IActionResult> GetBarberById(Guid barberId)
        {
            try
            {
                Barber barber = await _mediator.Send(new GetBarberByIdQuery(barberId));
                if (barber == null)
                {
                    ModelState.AddModelError("BarberNotFound", $"This appointment Id {barberId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(barber);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Add a new barber
        [HttpPost]
        [Route("addNewBarber")]
        public async Task<IActionResult> AddNewBarber([FromBody] BarberDto barberDto)
        {
            try
            {
                var validationResult = _validator.Validate(barberDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new AddNewBarberCommand(barberDto);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Update barber
        [HttpPost]
        [Route("updateBarber/{barberId}")]
        public async Task<IActionResult> UpdateBarber([FromBody] BarberDto barberDto, Guid barberId)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(barberDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new UpdateBarberCommand(barberDto, barberId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // delete barber
        [HttpDelete]
        [Route("deleteBarber/{barberId}")]
        public async Task<IActionResult> DeleteBarber(Guid barberId)
        {
            try
            {
                Barber barber = await _mediator.Send(new DeleteBarberCommand(barberId));
                if (barber == null)
                {
                    ModelState.AddModelError("BarberNotFound", $"This barber Id {barberId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(barber);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

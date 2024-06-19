
using Application.Commands.BarberShops.AddNewBarberShop;
using Application.Commands.BarberShops.DeleteBarberShop;
using Application.Commands.BarberShops.UpdateBarberShop;
using Application.Dtos;
using Application.Queries.BarberShops.GetAllBarberShops;
using Application.Queries.BarberShops.GetAllBarberShopStaff;
using Application.Queries.BarberShops.GetBarberShopById;
using Application.Validators.BarberShop;
using Domain.Models.Barbers;
using Domain.Models.BarberShops;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BarberShopController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberShopController : Controller
    {
        internal readonly IMediator _mediator;
        internal readonly BarberShopValidator _validator;

        public BarberShopController(IMediator mediator, BarberShopValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        //Get all barberShops
        [HttpGet]
        [Route("getAllBarberShops")]
        public async Task<IActionResult> GetAllBarberShops()
        {
            try
            {
                var query = new GetAllBarberShopsQuery();
                var result = await _mediator.Send(query);

                if (result is List<BarberShop> barberShops && barberShops.Count > 0)
                {
                    return Ok(barberShops);
                }
                ModelState.AddModelError("BarberShopNotFound", $"There is no barbershops found");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Get all barbershop's staf 
        [HttpGet]
        [Route("getAllBarberShopStaff/{barberShopName}")]
        public async Task<IActionResult> GetAllBarberShopStaff(string barberShopName)
        {
            try
            {
                var query = new GetAllBarberShopStaffQuery(barberShopName);
                var result = await _mediator.Send(query);

                if (result is List<Barber> barberShops && barberShops.Count > 0)
                {
                    return Ok(barberShops);
                }
                ModelState.AddModelError("BarberShopNoStaff", $"The barbershop '{barberShopName}' does not have any staff members.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Get a barberShop by id
        [HttpGet]
        [Route("getBarberShopById/{barberShopId}")]
        public async Task<IActionResult> GetBarberShopById(Guid barberShopId)
        {
            try
            {
                BarberShop barberShop = await _mediator.Send(new GetBarberShopByIdQuery(barberShopId));
                if (barberShop == null)
                {
                    ModelState.AddModelError("BarberShopNotFound", $"This appointment Id {barberShopId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(barberShop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Add a new barberShop
        [HttpPost]
        [Route("addNewBarberShop")]
        public async Task<IActionResult> AddNewBarberShop([FromBody] BarberShopDto barberShopDto)
        {
            try
            {
                var validationResult = _validator.Validate(barberShopDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new AddNewBarberShopCommand(barberShopDto);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Update barberShop
        [HttpPost]
        [Route("updateBarberShop/{barberShopId}")]
        public async Task<IActionResult> UpdateBarberShop([FromBody] BarberShopDto barberShopDto, Guid barberShopId)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(barberShopDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new UpdateBarberShopCommand(barberShopDto, barberShopId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // delete barberShop
        [HttpDelete]
        [Route("deleteBarberShop/{barberShopId}")]
        public async Task<IActionResult> DeleteBarberShop(Guid barberShopId)
        {
            try
            {
                BarberShop barberShop = await _mediator.Send(new DeleteBarberShopCommand(barberShopId));
                if (barberShop == null)
                {
                    ModelState.AddModelError("BarberShopNotFound", $"This barberShop Id {barberShopId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(barberShop);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

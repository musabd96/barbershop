using Application.Commands.Customers.AddCustomer;
using Application.Commands.Customers.DeleteCustomer;
using Application.Commands.Customers.UpdateCustomer;
using Application.Dtos;
using Application.Queries.Customers.GetAllCustomers;
using Application.Queries.Customers.GetCustomerById;
using Application.Validators.Customer;
using Domain.Models.Customers;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        internal readonly IMediator _mediator;
        internal readonly CustomerValidator _validator;

        public CustomerController(IMediator mediator, CustomerValidator validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        //Get all customers
        [HttpGet]
        [Route("getAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var query = new GetAllCustomersQuery();
                var result = await _mediator.Send(query);

                if (result is List<Customer> customers && customers.Count > 0)
                {
                    return Ok(customers);
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

        //Get a customer by id
        [HttpGet]
        [Route("getCustomerById/{customerId}")]
        public async Task<IActionResult> GetCustomerById(Guid customerId)
        {
            try
            {
                Customer customer = await _mediator.Send(new GetCustomerByIdQuery(customerId));
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerNotFound", $"This appointment Id {customerId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Add a new customer
        [HttpPost]
        [Route("addNewCustomer")]
        public async Task<IActionResult> AddNewCustomer([FromBody] CustomerDto customerDto)
        {
            try
            {
                var validationResult = _validator.Validate(customerDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new AddNewCustomerCommand(customerDto);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Update customer
        [HttpPost]
        [Route("updateCustomer/{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customerDto, Guid customerId)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(customerDto);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    return BadRequest(ModelState);
                }

                var command = new UpdateCustomerCommand(customerDto, customerId);
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // delete customer
        [HttpDelete]
        [Route("deleteCustomer/{customerId}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            try
            {
                Customer customer = await _mediator.Send(new DeleteCustomerCommand(customerId));
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerNotFound", $"This customer Id {customerId} is not found");
                    return BadRequest(ModelState);
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

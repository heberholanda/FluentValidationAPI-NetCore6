using FluentValidation;
using FluentValidationAPI.Models;
using FluentValidationAPI.Validation;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly IValidator<Customer> _validator;

        // Inject the validator through constructor
        public CustomerController(IValidator<Customer> validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer model)
        {
            // Validate the model using the injected validator
            var validationResult = await _validator.ValidateAsync(model);
            
            if (!validationResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, "Model is valid!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(Customer model)
        {
            // Validate the model using the injected validator
            var validatorResult = await _validator.ValidateAsync(model);

            if (!validatorResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validatorResult.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, "Model is valid for update!");
        }
    }
}

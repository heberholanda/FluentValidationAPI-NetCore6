using FluentValidationAPI.Models;
using FluentValidationAPI.Validation;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(Customer model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            return StatusCode(StatusCodes.Status200OK, "Model is valid!");
        }

        [HttpPut]
        public IActionResult Update(Customer model)
        {
            CustomerValidator customerValidator = new();
            var validatorResult = customerValidator.Validate(model);

            if (!validatorResult.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validatorResult.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, "Model is valid for update!");
        }
    }
}

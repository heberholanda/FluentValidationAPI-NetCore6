using FluentValidation;
using FluentValidationAPI.Models;

namespace FluentValidationAPI.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            // Validate that the address is not longer than 60 characters, as many carrier APIs don't allow more
            RuleFor(address => address.Street1).NotNull().NotEmpty().Length(1, 60);
            RuleFor(address => address.Street2).Length(1, 60);

            // Validate that the country is required
            RuleFor(address => address.Country).NotNull().NotEmpty().WithMessage("Please add the destination country");

            // Validate city and postal code
            RuleFor(address => address.PostalCode).NotNull().NotEmpty().WithMessage("Please add receiver postcode");
            RuleFor(address => address.PostalCode).Must(ValidPostCode).WithMessage("Postalcode is not valid");
            RuleFor(address => address.City).NotNull().NotEmpty().WithMessage("Please add the receiver city");
        }

        // Custom validation function to validate postal code
        private bool ValidPostCode(string postalCode)
        {
            // Add logic for validating postal code here...
            return true;
        }
    }
}

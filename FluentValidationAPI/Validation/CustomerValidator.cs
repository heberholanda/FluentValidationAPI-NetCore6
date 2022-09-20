using FluentValidation;
using FluentValidationAPI.Models;

namespace FluentValidationAPI.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            // Check name is not null, empty and is between 1 and 250 characters
            RuleFor(customer => customer.FirstName)
                .NotNull().WithMessage("FirstName field is required!")
                .NotEmpty().WithMessage("FirstName field is not emply!")
                .Length(1, 250);

            RuleFor(customer => customer.LastName).NotNull().NotEmpty().Length(1, 250);

            // Validate with a custom error message
            RuleFor(customer => customer.Phone).NotEmpty()
                .WithMessage("Please add a phone number");

            // Validate value between 21 and 100
            RuleFor(customer => customer.Age).InclusiveBetween(21, 100);

            // Validate the object (its a complex property)
            RuleFor(customer => customer.Address).InjectValidator();

            // Validate Enum Required
            RuleFor(customer => customer.RegistrationStatus).IsInEnum()
                .WithMessage("Not valid Status!");

            // Validate Enum
            RuleFor(customer => customer.RegistrationStatus).Must(x => x != RegistrationStatus.WaitingApproval)
                .WithMessage("Invalid Status!");

            // Validate Min Length
            RuleFor(customer => customer.FirstName).MinimumLength(3)
                .WithMessage("FirstName must be at least 3 characters long.");

            // Validate Max Length
            RuleFor(customer => customer.FirstName).MaximumLength(30)
                .WithMessage("FirstName can only have a maximum of 30 characters.");

            // Validate Great Than
            RuleFor(customer => customer.Credit).GreaterThan(10)
                .WithMessage("You must have at least 10 credits.");

            // Validate Less Than
            RuleFor(customer => customer.Credit).LessThan(100)
                .WithMessage("The maximum credit amount allowed is 100.");

            // Validate Email
            RuleFor(customer => customer.Email).EmailAddress()
                .WithMessage("Invalid email address!");

            // Validate Date Required
            RuleFor(customer => customer.StartDate)
                .NotEmpty().WithMessage("Start Date field is required.")
                .NotNull().WithMessage("Start Date cannot be null");

            // Validate Date Required
            RuleFor(customer => customer.EndDate)
                .NotEmpty().WithMessage("End Date field is required.")
                .NotNull().WithMessage("End Date cannot be null");

            // Validate EndDate > StartDate
            RuleFor(customer => customer).Must(customer => customer.EndDate == default(DateTime) || customer.StartDate == default(DateTime) || customer.EndDate > customer.StartDate)
                .WithMessage("End Date must greater than Start Date");

            // Validate with Function
            RuleFor(x => x.Enabled).Must(BeAValidEnabled)
                .WithMessage("Enabled must be True;");

            // Validate with Function & parameters
            RuleFor(v => v.Id).Must(
                (model, id) => IsUniqueUserName(model.Age, id))
                .WithMessage("{PropertyName} must be greater than 0.");
        }


        private bool IsUniqueUserName(int age, int id)
        {
            if (age > 18 && id > 0)
                return true;
            return false;
        }

        private bool BeAValidEnabled(bool status)
        {
            return status;
        }
    }
}

using FirmaRest.Models.DTO;
using FluentValidation;

namespace FirmaRest.DataValidation.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .Matches("^[A-Za-z]+$");

            RuleFor(e => e.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .Matches("^[A-Za-z]+$");

            RuleFor(e => e.Title)
                .MaximumLength(10)
                .Matches("^[a-zA-Z]+[.]?$");

            RuleFor(e => e.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.Contact)
                .NotEmpty()
                .NotNull()
                .Matches(@"^[+]?\d{10,}$")
                .MaximumLength(15);
        }
    }
}

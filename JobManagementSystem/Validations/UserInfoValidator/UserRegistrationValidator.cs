
using FluentValidation;
using JobManagementSystem.Models;

namespace JobManagementSystem.API.Validations.UserInfoValidator
{
    public class UserRegistrationValidator : AbstractValidator<UserRegistrationDTO>
    {
        public UserRegistrationValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("FirstName must contain letters");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("LastName must contain letters");
            RuleFor(user => user.UserName).NotEmpty().WithMessage("UserName must contain letters");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email address is required.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Invalid email address.");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Your password Should not be empty")
                .Must(pass => pass.Length >= 8).WithMessage("Your password must contain 8 simbols");
            RuleFor(user => user.IsEmployer)
            .NotNull().WithMessage("IsEmployer must be specified.");
            RuleFor(user => user.Company)
            .NotEmpty().When(user => user.IsEmployer).WithMessage("Company name is required when IsEmployer is true.");
        }
    }
}

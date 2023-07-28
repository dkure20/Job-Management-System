using FluentValidation;
using JobManagementSystem.API.Models;

namespace JobManagementSystem.API.Validations.UserInfoValidator
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email field mustnt be empty");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password field mustnt be empty");

        }
    }
}

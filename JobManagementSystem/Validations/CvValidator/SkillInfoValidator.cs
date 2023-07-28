using FluentValidation;
using FluentValidation.Validators;
using JobManagementSystem.Domain.CvModel;

namespace JobManagementSystem.API.Validations.CvValidator
{
    public class SkillInfoValidator : AbstractValidator<SkillInfo>
    {
        public SkillInfoValidator()
        {
            RuleFor(x => x.Experience)
            .NotEmpty().WithMessage("Experience is required.")
            .When(x => x.SkillId!=null);
        }
    }
}
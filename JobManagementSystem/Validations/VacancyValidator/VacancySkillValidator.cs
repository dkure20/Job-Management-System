using FluentValidation;
using FluentValidation.Validators;
using JobManagementSystem.Domain.VacancyModels;

namespace JobManagementSystem.API.Validations.VacancyValidator
{
    public class VacancySkillValidator : AbstractValidator<VacancySkills>
    {
        public VacancySkillValidator() 
        {
            RuleFor(x => x.Experience)
            .NotEmpty().WithMessage("Experience is required.")
            .When(x => x.SkillId != null);
        }
    }
}
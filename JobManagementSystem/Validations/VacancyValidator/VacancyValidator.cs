using FluentValidation;
using JobManagementSystem.Domain.VacancyModels;

namespace JobManagementSystem.API.Validations.VacancyValidator
{
    public class VacancyValidator : AbstractValidator<Vacancy>
    {
        public VacancyValidator()
        {
            RuleFor(vacancy => vacancy.JobTable.JobTitle)
            .NotEmpty().WithMessage("Job title is required.")
            .MaximumLength(100).WithMessage("Job title cannot exceed 100 characters.");
            RuleFor(vacancy => vacancy.JobTable.ExpireDate)
                .NotEmpty().WithMessage("Expiration date is required.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Expiration date must be in the future.");
            RuleForEach(vacancy => vacancy.SkillTable.skills).SetValidator(new VacancySkillValidator());

        }
    }
}

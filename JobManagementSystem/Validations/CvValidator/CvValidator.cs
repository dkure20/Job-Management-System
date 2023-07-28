using FluentValidation;
using JobManagementSystem.Domain.CvModel;

namespace JobManagementSystem.API.Validations.CvValidator
{
    public class CvValidator : AbstractValidator<Cv>
    {
        public CvValidator() 
        {
            RuleFor(x => x).Must(x => DateValidation(x));
            RuleForEach(x => x.SkillTable.UserSkills).SetValidator(new SkillInfoValidator());

        }

        private bool DateValidation(Cv x)
        {
            return x.EndDate < DateTime.Now && x.EndDate > x.StartDate;
        }
    }
}

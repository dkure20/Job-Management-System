using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Domain.VacancyModels
{
    public class Vacancy
    {
        public JobTable JobTable { get; set; }
        public VacancySkillTable SkillTable { get; set; }

    }
}

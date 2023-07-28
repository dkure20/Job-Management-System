using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Domain.VacancyModels
{
    public class JobAllInformation
    {
        public int JobId { get; set; }
        public string JobTitle { get;set; }
        public DateTime ExpireDate { get; set; }
        public string SkillName { get; set; }
        public int Experience { get; set; }
        public int Weight { get; set; }
    }
}

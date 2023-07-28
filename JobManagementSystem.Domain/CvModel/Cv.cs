using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Domain.CvModel
{
    public class Cv
    {
        public int EducationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DegreeId { get; set; }
        public int ProfessionId { get; set; }
        public SkillTableModel SkillTable { get; set; }

    }
}

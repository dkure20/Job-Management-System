using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.JobRepoModels
{
    public class ResumeModel
    {
        public ResumeModel()
        {
            
        }
        public List<Skills> SkillList { get; set; }
        public List<Profession> ProfessionList { get; set; }
        public List<Degree> DegreeList { get; set; }
        public List<Education> EducationList { get; set; }
    }
}

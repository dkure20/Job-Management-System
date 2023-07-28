using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Repository.JobRepoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Services.JobService
{
    public interface IJobService
    {
       public Task AddUserSkills(int userId, SkillTableModel skillTable);
        public Task CreateUserCv(int userId, Cv cvModel);
        public Task DeleteCv(int userId);
        public Task<ResumeModel> GetResume();
        public Task UpdateCv(int userId, Cv model);
    }
}

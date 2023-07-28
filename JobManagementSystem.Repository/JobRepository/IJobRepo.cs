using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Repository.JobRepoModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.JobRepo
{
    public interface IJobRepo
    {
        Task<ResumeModel> GetResumeDetails(NpgsqlConnection connection);
        Task CreateUserCv(int UserId,ResumeTableModel resumeModel, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task CreateUserSkills(int userId, SkillInfo userSkills, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task<int> FindUser(int userId, NpgsqlConnection connection);
        Task DeleteUserCv(int userId, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task UpdateUserCv(int userId, ResumeTableModel model, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task UpdateSkill(int userId, SkillInfo skill, NpgsqlConnection connection, NpgsqlTransaction transaction);
    }
}

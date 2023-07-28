using JobManagementSystem.Domain.VacancyModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.EmployerRepo
{
    public interface IEmpRepo
    {
        Task CreateJobSkillTable(int jobId, VacancySkills skill, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task<int> CreateJobTable(int userId, JobTable jobTable, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task<int> DeleteJob(int managerId, int jobId, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task DeleteJobSkills(int deletedRow, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task<int> FindJob(int jobId, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task<List<JobAllInformation>> GetAllJobs(NpgsqlConnection connection);
        Task<List<TopUserModel>> GetTopUsers(int jobId, NpgsqlConnection transaction);
        Task RegisterUserOnVacancy(NpgsqlConnection connection, NpgsqlTransaction transaction,int jobId, int userId);
        Task<int> UpdateJobTable(int jobId, int employerId,JobTable jobTable, NpgsqlConnection connection, NpgsqlTransaction transaction);
        Task UpdateSkillTable(int jobId, VacancySkills skill, NpgsqlConnection connection, NpgsqlTransaction transaction);
    }
}

using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Repository.JobRepo;
using JobManagementSystem.Repository.JobRepoModels;
using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.JobRepo
{
    public class JobCacheRepository : IJobRepo
    {
        private readonly IJobRepo _jobRepo;
        private readonly IMemoryCache _memoryCache;
        public JobCacheRepository(IJobRepo jobRepo, IMemoryCache memoryCache)
        {
            _jobRepo = jobRepo;
            _memoryCache = memoryCache;
        }
        public Task CreateUserCv(int UserId, ResumeTableModel resumeModel, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            return _jobRepo.CreateUserCv(UserId, resumeModel, connection, transaction);
        }

        public Task CreateUserSkills(int userId, SkillInfo userSkills, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            return _jobRepo.CreateUserSkills(userId, userSkills, connection, transaction);
        }

        public Task DeleteUserCv(int userId, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
           return _jobRepo.DeleteUserCv(userId, connection, transaction);
        }

        public Task<int> FindUser(int userId, NpgsqlConnection connection)
        {
            string key = $"user-{userId}";
            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                    return _jobRepo.FindUser(userId, connection);
                });
        }

        public Task<ResumeModel> GetResumeDetails(NpgsqlConnection connection)
        {
            return _jobRepo.GetResumeDetails(connection);
        }

        public Task UpdateSkill(int userId, SkillInfo skill, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
           return _jobRepo.UpdateSkill(userId, skill, connection, transaction);
        }

        public Task UpdateUserCv(int userId, ResumeTableModel model, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
           return _jobRepo.UpdateUserCv(userId, model, connection, transaction);
        }
    }
}

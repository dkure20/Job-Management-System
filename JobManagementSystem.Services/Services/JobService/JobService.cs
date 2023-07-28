using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Repository;
using JobManagementSystem.Repository.JobRepo;
using JobManagementSystem.Repository.JobRepoModels;
using JobManagementSystem.Repository.RepoModels.Config;
using JobManagementSystem.Services.JWT;
using Microsoft.Extensions.Options;
using Npgsql;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManagementSystem.Repository.RepoModels;
using Microsoft.AspNetCore.Authorization;
using JobManagementSystem.Services.Exceptions;

namespace JobManagementSystem.Services.Services.JobService
{
    public class JobService : IJobService
    {
        private readonly IJobRepo _repo;
        private readonly Connection _connection;
        public JobService(IJobRepo repo, IOptions<Connection> connectionString)
        {
            _connection = connectionString.Value;
            _repo = repo;
        }

        public async Task AddUserSkills(int userId, SkillTableModel skillTable)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                int id = await _repo.FindUser(userId, connection);
                if (id == 0) throw new UserDoesNotExist("User doesn't have cv", 400);
                using (var transaction = connection.BeginTransaction())
                {
                    for (int i = 0; i < skillTable.UserSkills.Count; i++)
                    {
                        SkillInfo skillInfo = skillTable.UserSkills[i];
                        await _repo.CreateUserSkills(userId, skillInfo, connection, transaction);
                    }
                    transaction.Commit();
                }
            }
        }

        public async Task CreateUserCv(int userId, Cv cvModel)
        {
            var resumeModel = cvModel.Adapt<ResumeTableModel>();
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                int id = await _repo.FindUser(userId,connection);
                if (id != 0) throw new UserAlreadyExists();
                using (var transaction = connection.BeginTransaction())
                {
                    await _repo.CreateUserCv(userId,resumeModel,connection,transaction);
                    for(int i=0; i< cvModel.SkillTable.UserSkills.Count; i++)
                    {
                        SkillInfo skillInfo = cvModel.SkillTable.UserSkills[i];
                        await _repo.CreateUserSkills(userId, skillInfo, connection, transaction);
                    }
                    transaction.Commit();
                }
            }
        }

        public async Task DeleteCv(int userId)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                int id = await _repo.FindUser(userId, connection);
                if (id == 0) throw new UserDoesNotExist("User doesn't have cv",400);
                using(var transaction = connection.BeginTransaction())
                {
                    await _repo.DeleteUserCv(userId,connection,transaction);
                    transaction.Commit();
                }
            }
        }

        public async Task<ResumeModel> GetResume()
        {
            using(var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                return await _repo.GetResumeDetails(connection);
            }
           
        }

        public async Task UpdateCv(int userId, Cv CvModel)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                int id = await _repo.FindUser(userId, connection);
                if (id == 0) throw new UserDoesNotExist("User doesn't have cv to edit", 400);
                var model = CvModel.Adapt<ResumeTableModel>();
                using (var transaction = connection.BeginTransaction())
                {
                    await _repo.UpdateUserCv(userId, model, connection, transaction);
                    for(int i = 0; i < CvModel.SkillTable.UserSkills.Count; i++)
                    {
                        var skill = CvModel.SkillTable.UserSkills[i];
                        await _repo.UpdateSkill(userId, skill, connection, transaction);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}

using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Domain.VacancyModels;
using JobManagementSystem.Repository.EmployerRepo;
using JobManagementSystem.Repository.RepoModels.Config;
using JobManagementSystem.Services.Exceptions;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Services.VacancyService
{
    public class VacancyService : IVacancyService
    {
        private readonly IEmpRepo _empRepo;
        private readonly Connection _connection;
        public VacancyService(IEmpRepo empRepo, IOptions<Connection> connectionString)
        {
            _connection = connectionString.Value;
            _empRepo = empRepo;
        }

        public async Task AddVacancy(int userId, Vacancy vacancy)
        {
            using(var connection = new NpgsqlConnection(_connection.ConnectionString)) 
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                   int jobId =  await _empRepo.CreateJobTable(userId,vacancy.JobTable, connection,transaction);
                    for(int i=0; i<vacancy.SkillTable.skills.Count; i++)
                    {
                        VacancySkills skill = vacancy.SkillTable.skills[i];
                        await _empRepo.CreateJobSkillTable(jobId,skill, connection,transaction);
                    }
                    transaction.Commit();
                }

            }
            
        }

        public async Task DeleteJobInformation(int managerId, int jobId)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    int deletedRow = await _empRepo.DeleteJob(managerId, jobId, connection, transaction);
                    if (deletedRow == 0) throw new UserDoesNotExist("You can't delete that job", 400);
                    await _empRepo.DeleteJobSkills(deletedRow,connection,transaction);
                    transaction.Commit();
                }

            }
        }

        public async Task<List<TopUserModel>> GetBestApplicants(int jobId)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                List<TopUserModel> TopUsers = await _empRepo.GetTopUsers(jobId, connection);
                return TopUsers;
            }
        }

        public async Task<List<JobAllInformation>> GetJobsInfo()
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                List<JobAllInformation> jobs = await _empRepo.GetAllJobs(connection);
                return jobs;
            }
        }

        public async Task RegisterUser(int jobId, int userId)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    int id = await _empRepo.FindJob(jobId,connection, transaction);
                    if (id == 0) throw new UserDoesNotExist("That type of vacancy does not exist", 400);
                    await _empRepo.RegisterUserOnVacancy(connection,transaction,jobId, userId);
                    transaction.Commit();
                }

            }
        }


        public async Task UpdateVacancy(int jobId, int employerId, Vacancy model)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                   int rowsAffected = await _empRepo.UpdateJobTable(jobId,employerId, model.JobTable, connection, transaction);
                    if (rowsAffected == 0) throw new UserDoesNotExist("Manager doesnt have that job vacancy",400);
                    for(int i=0; i<model.SkillTable.skills.Count; i++)
                    {
                        VacancySkills skill = model.SkillTable.skills[i];
                        await _empRepo.UpdateSkillTable(jobId,skill,connection, transaction);
                    }

                    transaction.Commit();
                }
            }
        }
    }
}

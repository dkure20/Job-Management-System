using Dapper;
using JobManagementSystem.Domain.VacancyModels;
using JobManagementSystem.Repository.RepoModels.Config;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.EmployerRepo
{
    public class EmpRepo : IEmpRepo
    {
        public async Task CreateJobSkillTable(int jobId, VacancySkills skill, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"insert into job_skills (job_id, skill_id, experience,weight) VALUES (@jobId,@SkillId,@Experience,@Weight)";
            await connection.ExecuteAsync(query, new { jobId, skill.SkillId, skill.Experience,skill.Weight } , transaction);
        }

        public async Task<int> CreateJobTable(int userId,JobTable jobTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"insert into jobs(manager_id, job_title, expire_date) VALUES(@userId, @JobTitle, @ExpireDate) returning job_id";
            int id = await connection.ExecuteAsync(query, new {userId,jobTable.JobTitle,jobTable.ExpireDate}, transaction);
            return id;
        }

        public async Task<int> DeleteJob(int managerId, int jobId, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"delete from jobs where job_id = @JobId and manager_id = @ManagerId returning job_id";
            int deletedRow = await connection.ExecuteAsync(query, new {jobId,managerId}, transaction);
            return deletedRow;
        }

        public async Task DeleteJobSkills(int DeletedRow, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"delete from job_skills where job_id = @DeletedRow";
            await connection.ExecuteAsync (query, new {DeletedRow},transaction);
        }

        public async Task<int> FindJob(int jobId, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"select job_id from jobs where job_id = @JobId";
            int job = await connection.QuerySingleOrDefaultAsync<int>(query, new { jobId });
            return job;
        }

        public async Task<List<JobAllInformation>> GetAllJobs(NpgsqlConnection connection)
        {
            var query = $"select j.job_id as JobId, j.job_title as JobTitle, j.expire_date as ExpireDate, s.skill_name as SkillName, js.experience, js.weight from jobs j left join job_skills js on j.job_id = js.job_id left join skills s on js.skill_id = s.id";
            List<JobAllInformation> jobAllInformation = (await connection.QueryAsync<JobAllInformation>(query)).ToList();
            return jobAllInformation;

        }

        public async Task<List<TopUserModel>> GetTopUsers(int jobId,NpgsqlConnection connection)
        {
            var query = $"select l.UserId as UserId, r.first_name as FirstName, r.last_name as LastName, r.email as Email, l.score as Score\r\nfrom (select a.UserId, sum(((cast(a.experience as decimal) / j.experience) * j.weight)) as score\r\n      from (select s.user_id as UserId, skill_id as SkillId, experience, ra.job_id\r\n            from skill_table s\r\n                     left join registered_applicants ra on s.user_id = ra.user_id\r\n            where job_id = @jobId) as a\r\n               join job_skills j on a.job_id = j.job_id and j.skill_id = a.SkillId\r\n      group by a.UserId\r\n      ) as l left join registered_user r on l.UserId = r.id order by score desc limit 10";
            List<TopUserModel> topUsers = (await connection.QueryAsync<TopUserModel> (query, new {jobId})).ToList();
            return topUsers;
        }

        public async Task RegisterUserOnVacancy(NpgsqlConnection connection, NpgsqlTransaction transaction, int jobId, int userId)
        {
            var query = $"insert into registered_applicants(job_id, user_id) VALUES (@jobId,@userId)";
            await connection.ExecuteAsync(query, new { jobId, userId }, transaction);
        }

        public async Task<int> UpdateJobTable(int jobId, int employerId,JobTable jobTable, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"update jobs set job_title = @JobTitle, expire_date = @ExpireDate where job_id = @jobId and manager_id = @employerId";
            int changedRows = await connection.ExecuteAsync(query, new { jobTable.JobTitle, jobTable.ExpireDate, jobId,employerId }, transaction);
            return changedRows;
        }

        public async Task UpdateSkillTable(int jobId, VacancySkills skill, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"update job_skills set experience = @Experience, weight = @Weight where job_id = @jobId and skill_id = @SkillId";
            await connection.ExecuteAsync(query, new { skill.Experience, skill.Weight, jobId, skill.SkillId }, transaction);
        }
    }
}

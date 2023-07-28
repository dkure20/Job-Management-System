using Dapper;
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
    public class JobRepo : IJobRepo
    {

        public async Task CreateUserCv(int UserId, ResumeTableModel resumeModel, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"insert into resume_table (user_id, education_id, start_date, end_date, degree_id, profession_id) values (@UserId,@EducationId,@StartDate,@EndDate,@DegreeId,@ProfessionId)";
            await connection.ExecuteAsync(query, new {UserId,resumeModel.EducationId,resumeModel.StartDate,resumeModel.EndDate,resumeModel.DegreeId,resumeModel.ProfessionId},transaction);
        }
            
        public async Task CreateUserSkills(int UserId, SkillInfo userSkills, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"INSERT INTO skill_table (user_id, skill_id, experience) VALUES (@UserId,@SkillId,@Experience)";
            await connection.ExecuteAsync(query,new {UserId,userSkills.SkillId,userSkills.Experience},transaction);
        }

        public async Task DeleteUserCv(int userId, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var resumeQuery = $"delete from resume_table where user_id = @userId";
            var skillQuery = $"delete from skill_table where user_id = @userId";
            await connection.ExecuteAsync(resumeQuery,new {userId},transaction);
            await connection.ExecuteAsync(skillQuery, new {userId}, transaction);
        }

        public async Task<int> FindUser(int userId, NpgsqlConnection connection)
        {
            var query = $"select user_id from resume_table where user_id = @userId";
            int id = await connection.QuerySingleOrDefaultAsync<int>(query,new {userId});
            return id;
        }

        public async Task<ResumeModel> GetResumeDetails(NpgsqlConnection connection)
        {
            ResumeModel resumeModel = new ResumeModel();
            var query1 = $"select id as Id, skill_name as SkillName from skills";
            resumeModel.SkillList = (await connection.QueryAsync<Skills>(query1)).ToList();
            var query2 = $"select id as Id, degree_name as DegreeName from degrees";
            resumeModel.DegreeList = (await connection.QueryAsync<Degree>(query2)).ToList();
            var query3 = $"select id as Id, profession_name as ProfessionName from professions";
            resumeModel.ProfessionList = (await connection.QueryAsync<Profession>(query3)).ToList();
            var query4 = $"select id as Id, education_name as EducationName from education";
            resumeModel.EducationList = (await connection.QueryAsync<Education>(query4)).ToList();
            return resumeModel;

        }

        public async Task UpdateSkill(int UserId, SkillInfo skill, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"update skill_table set experience = @Experience where user_id = @UserId and skill_id = @SkillId";
            await connection.ExecuteAsync(query, new { skill.Experience, UserId, skill.SkillId }, transaction);
        }

        public async Task UpdateUserCv(int userId, ResumeTableModel model, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = $"update resume_table set education_id = @EducationId,start_date= @StartDate,end_date = @EndDate, degree_id= @DegreeId,profession_id = @ProfessionId where user_id = @UserId";
            await connection.ExecuteAsync(query,new {model.EducationId, model.StartDate,model.EndDate,model.DegreeId,model.ProfessionId,userId},transaction);
        }
    }
}

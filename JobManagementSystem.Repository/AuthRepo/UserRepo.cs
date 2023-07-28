using Dapper;
using JobManagementSystem.Repository.AuthRepoModels;
using JobManagementSystem.Repository.RepoModels.Config;
using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository
{
    public class UserRepo : IUserRepo
    { 
        public async Task<RegisteredUser?> FindUser(UserLoginRepoModel userModel, NpgsqlConnection connection)
        {
            
             var query = @"select id, is_employer as IsEmployer from registered_user where email = @Email and password = @Password";
             RegisteredUser registeredUser = await connection.QuerySingleOrDefaultAsync<RegisteredUser>(query, new { userModel.Email, userModel.Password });
             return registeredUser;
        }

        public async Task<int> RegisterUserInTable(UserRepoModel userRegister, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            var query = @"INSERT INTO registered_user
                    (first_name, last_name, user_name, email, password, is_employer, company) 
                    VALUES (@FirstName,@LastName,@UserName,@Email,@Password,@IsEmployer,@Company) returning id";
            int registrationId = await connection.ExecuteScalarAsync<int>(query, new { userRegister.FirstName, userRegister.LastName, userRegister.UserName, userRegister.Email, userRegister.Password, userRegister.IsEmployer, userRegister.Company },transaction);
            return registrationId;
        }
    }
}

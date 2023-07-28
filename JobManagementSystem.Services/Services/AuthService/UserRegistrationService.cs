using Mapster;
using JobManagementSystem.Services.ExtentionMethods;
using JobManagementSystem.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design.Serialization;
using JobManagementSystem.Services.Exceptions;
using System.Reflection.Emit;
using JobManagementSystem.Services.JWT;
using JobManagementSystem.Repository.AuthRepoModels;
using JobManagementSystem.Repository;
using JobManagementSystem.Repository.RepoModels.Config;
using Microsoft.Extensions.Options;
using Npgsql;
using JobManagementSystem.Repository.RepoModels.UserRepoModels;

namespace JobManagementSystem.Services.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepo _repo;
        private readonly IJwt _jwt;
        private readonly Connection _connection;
        public UserRegistrationService(IUserRepo repo, IJwt jwt, IOptions<Connection> connectionString)
        {
            _connection = connectionString.Value;
            _jwt = jwt;
            _repo = repo;
        }

        public async Task<String?> Login(UserLoginServiceMod userModel)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                string hashPassword = userModel.Password.HashPassword();
                userModel.Password = hashPassword;
                var model = userModel.Adapt<UserLoginRepoModel>();
                RegisteredUser user = await _repo.FindUser(model,connection);
                if (user == null) throw new UserDoesNotExist("User is not registered",400);
                return _jwt.GenerateJwt(user);

            }
        }

        public async Task<int> RegisterUser(UserRegistrationModel user)
        {
            using (var connection = new NpgsqlConnection(_connection.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string password = user.Password.HashPassword();
                    user.Password = password;
                    var repoModel = user.Adapt<UserRepoModel>();
                    int id = await _repo.RegisterUserInTable(repoModel,connection,transaction);
                    transaction.Commit();
                    return id;
                }
            }
        }
    }
}

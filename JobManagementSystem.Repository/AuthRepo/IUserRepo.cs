using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository
{
    public interface IUserRepo
    {
        Task<RegisteredUser?> FindUser(UserLoginRepoModel userModel, NpgsqlConnection connection);
        Task<int> RegisterUserInTable(UserRepoModel repoModel, NpgsqlConnection connection, NpgsqlTransaction transaction);
    }
}

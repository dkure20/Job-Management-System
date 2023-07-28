using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using Microsoft.Extensions.Caching.Memory;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Repository.AuthRepoModels
{
    public class CachedRepository : IUserRepo
    {
        private readonly IUserRepo _userRepo;
        private readonly IMemoryCache _memoryCache;
        public CachedRepository(IUserRepo userRepo, IMemoryCache memoryCache)
        {
            _userRepo = userRepo;
            _memoryCache = memoryCache;
        }
        public Task<RegisteredUser?> FindUser(UserLoginRepoModel userModel, NpgsqlConnection connection)
        {
            string key = $"member-{userModel.Email}-{userModel.Password}";
            return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                    return _userRepo.FindUser(userModel,connection);
                });
        }

        public Task<int> RegisterUserInTable(UserRepoModel repoModel, NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            return _userRepo.RegisterUserInTable(repoModel, connection, transaction);
        }
    }
}

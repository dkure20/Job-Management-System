using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.JWT
{
    public interface IJwt
    {
        public string GenerateJwt(RegisteredUser user);
    }
}

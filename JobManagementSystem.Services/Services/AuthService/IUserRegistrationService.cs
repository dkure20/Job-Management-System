using JobManagementSystem.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Services
{
    public interface IUserRegistrationService
    {
        Task<string?> Login(UserLoginServiceMod userModel);
        Task<int> RegisterUser(UserRegistrationModel user);
    }
}

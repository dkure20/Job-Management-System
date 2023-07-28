using JobManagementSystem.API.Models;
using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Models;
using JobManagementSystem.Repository.RepoModels.UserRepoModels;
using JobManagementSystem.Services.ServiceModels;
using Mapster;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.ExtentionMethods
{
    public class MappingConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<UserRegistrationDTO, UserRegistrationModel>
                .NewConfig()
                .Compile();
            TypeAdapterConfig<UserRegistrationModel, UserRepoModel>
                .NewConfig()
                .Compile();
            TypeAdapterConfig<UserLoginDTO, UserLoginServiceMod>
                .NewConfig()
                .Compile();
            TypeAdapterConfig<UserLoginServiceMod, UserLoginRepoModel>
                .NewConfig()
                .Compile();
            //TypeAdapterConfig<Cv, ResumeTableModel>
            //    .NewConfig()
            //    .Compile();
        }
    }
}

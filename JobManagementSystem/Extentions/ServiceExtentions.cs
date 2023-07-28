using FluentValidation;
using JobManagementSystem.API.Models;
using JobManagementSystem.API.Validations.CvValidator;
using JobManagementSystem.API.Validations.UserInfoValidator;
using JobManagementSystem.API.Validations.VacancyValidator;
using JobManagementSystem.Domain.CvModel;
using JobManagementSystem.Domain.VacancyModels;
using JobManagementSystem.Models;
using JobManagementSystem.Repository;
using JobManagementSystem.Repository.AuthRepoModels;
using JobManagementSystem.Repository.EmployerRepo;
using JobManagementSystem.Repository.JobRepo;
using JobManagementSystem.Services.ExtentionMethods;
using JobManagementSystem.Services.JWT;
using JobManagementSystem.Services.Services;
using JobManagementSystem.Services.Services.JobService;
using JobManagementSystem.Services.Services.VacancyService;
using Microsoft.Extensions.Caching.Memory;

namespace JobManagementSystem.Extentions
{
    public static class ServiceExtentions
    {
        public static void AddCustomServices(this IServiceCollection Services)
        {
            Services.AddSingleton<IUserRepo,UserRepo>();
            Services.AddSingleton<IMemoryCache,MemoryCache>();
            Services.Decorate<IUserRepo,CachedRepository>();
            Services.AddSingleton<IJobService,JobService>();
            Services.AddSingleton<IJobRepo,JobRepo>();
            Services.Decorate<IJobRepo,JobCacheRepository>();
            MappingConfig.Configure();
            Services.AddScoped<IValidator<UserRegistrationDTO>, UserRegistrationValidator>();
            Services.AddScoped<IValidator<Cv>, CvValidator>();
            Services.AddScoped<IValidator<UserLoginDTO>, UserLoginValidator>();
            Services.AddSingleton<IUserRegistrationService, UserRegistrationService>();
            Services.AddSingleton<IJwt, JwtGenerator>();
            Services.AddSingleton<IVacancyService,VacancyService>();
            Services.AddSingleton<IEmpRepo,EmpRepo>();
            Services.AddScoped<IValidator<Vacancy>, VacancyValidator>();

        }
    }
}

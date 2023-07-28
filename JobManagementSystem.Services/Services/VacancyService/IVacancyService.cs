using JobManagementSystem.Domain.VacancyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManagementSystem.Services.Services.VacancyService
{
    public interface IVacancyService
    {
        Task AddVacancy(int userId, Vacancy vacancy);
        Task DeleteJobInformation(int managerId, int jobId);
        Task<List<TopUserModel>> GetBestApplicants(int jobId);
        Task<List<JobAllInformation>> GetJobsInfo();
        Task RegisterUser(int jobId, int userId);
        Task UpdateVacancy(int employerId, int employerId1, Vacancy model);
    }
}

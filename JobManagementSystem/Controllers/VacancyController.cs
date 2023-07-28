using JobManagementSystem.Domain.VacancyModels;
using JobManagementSystem.Services.Services.VacancyService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using JobManagementSystem.Domain.CvModel;

namespace JobManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;
        private readonly IValidator<Vacancy> _validator;
        public VacancyController(IVacancyService vacancyService,IValidator<Vacancy> validator)
        {
            _validator = validator;
            _vacancyService = vacancyService;
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> AddVacancy(Vacancy vacancy)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            ValidationResult res = _validator.Validate(vacancy);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _vacancyService.AddVacancy(userId,vacancy);
            return Ok();
        }
        [HttpPost("[action]/{jobId}")]
        [Authorize(Roles = "Applicant")]    
        public async Task<IActionResult> RegisterUserOnVacancy(int jobId)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            await _vacancyService.RegisterUser(jobId,userId);
            return Ok();
        }
        [HttpGet("[action]/{jobId}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetBestApplicants(int jobId)
        { 
            return Ok(await _vacancyService.GetBestApplicants(jobId));
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllJobInformation()
        {
            return Ok(await _vacancyService.GetJobsInfo());
        }
        [HttpDelete("[action]/{jobId}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            int managerId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            await _vacancyService.DeleteJobInformation(managerId,jobId);
            return Ok();
        }
        [HttpPut("[action]/{jobId}")]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> UpdateVacancy(int jobId,Vacancy model)
        {
            int employerId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            ValidationResult res = _validator.Validate(model);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _vacancyService.UpdateVacancy(jobId,employerId, model);
            return Ok();
        }

    }
}

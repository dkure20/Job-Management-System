using FluentValidation;
using FluentValidation.Results;
using JobManagementSystem.Models;
using JobManagementSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using JobManagementSystem.Services.ServiceModels;
using Mapster;
using JobManagementSystem.API.Models;
using JobManagementSystem.Services.Services.JobService;
using JobManagementSystem.Domain.CvModel;
using Microsoft.AspNetCore.Authorization;

namespace JobManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController : ControllerBase
    {
        private readonly IValidator<Cv> _validator;
        private readonly IJobService _jobService;
        public ResumeController(IJobService jobService, IValidator<Cv> validator)
        {
            _validator = validator;
            _jobService = jobService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> GetCvInformation()
        { 
            return Ok(await _jobService.GetResume());    
        }
        [HttpPut("[action]")]
        [Authorize (Roles = "Applicant")]
        public async Task<IActionResult> CreateCv(Cv CvModel)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            ValidationResult res = _validator.Validate(CvModel);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _jobService.CreateUserCv(userId,CvModel);
            return Ok();
        }
        [HttpDelete("[action]")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> DeleteResume()
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            await _jobService.DeleteCv(userId);
            return Ok();
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> UpdateResume(Cv model)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            ValidationResult res = _validator.Validate(model);
            if (!res.IsValid) return BadRequest(res.Errors);
            await _jobService.UpdateCv(userId,model);
            return Ok();
        }
        [HttpPut("[action]")]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> AddSkill(SkillTableModel skillTable)
        {
            int userId = int.Parse(this.User.Claims.First(i => i.Type == "id").Value);
            await _jobService.AddUserSkills(userId, skillTable);
            return Ok();
        }
    }
}

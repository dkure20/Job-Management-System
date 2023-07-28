using FluentValidation;
using FluentValidation.Results;
using JobManagementSystem.Models;
using JobManagementSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;
using JobManagementSystem.Services.ServiceModels;
using Mapster;
using JobManagementSystem.API.Models;

namespace JobManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IValidator<UserRegistrationDTO> _validator;
        private readonly IValidator<UserLoginDTO> _validator2;
        private readonly IUserRegistrationService _userRegistration;
        public UserController(IValidator<UserRegistrationDTO> validator, IUserRegistrationService userRegistration, IValidator<UserLoginDTO> validator2)
        {
            _validator2= validator2;
            _userRegistration = userRegistration;
            _validator = validator;

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> registerUser(UserRegistrationDTO user)
        {
            ValidationResult res = _validator.Validate(user);
            if (!res.IsValid) return BadRequest(res.Errors);
            var userModel = user.Adapt<UserRegistrationModel>();
            var id = await _userRegistration.RegisterUser(userModel);
            return Ok(id);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser(UserLoginDTO user)
        {
            ValidationResult res = _validator2.Validate(user);
            if (!res.IsValid) return BadRequest(res.Errors);
            var userModel = user.Adapt<UserLoginServiceMod>();
            var token = await _userRegistration.Login(userModel);
            return Ok(token);
        }
    }
}

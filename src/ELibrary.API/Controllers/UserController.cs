using ELibrary.Business.Helpers.GenerateJWT;
using ELibrary.Business.Models.User;
using ELibrary.Business.Services.Interface;
using ELibrary.Business.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly IValidator<CreateUserModel> _registerValidator;
        private readonly IValidator<LoginUserModel> _loginValidator;

        public UserController(
            IUserService userService,
            IValidator<CreateUserModel> registerValidator,
            IValidator<LoginUserModel> loginValidator)
        {
            _userService = userService;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel model)
        {
            var validation = await _registerValidator.ValidateAsync(model);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var result = await _userService.RegisterAsync(model);
            if (!result.Succedded)
                return BadRequest(result.Errors);
            return Ok(result.Result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            var result = await _userService.LoginAsync(model);
            if (!result.Succedded)
                return Unauthorized(result.Errors);
            return Ok(result.Result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = Guid.Parse(User.FindFirst(CustomClaimNames.Id)!.Value);
            var result = await _userService.GetProfileAsync(userId);
            if (!result.Succedded)
                return NotFound(result.Errors);
            return Ok(result.Result);
        }
    }
}

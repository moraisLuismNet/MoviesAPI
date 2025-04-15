using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userService.ValidateUserService(userRegistrationDTO))
            {
                return BadRequest(_userService.Errors);
            }

            userRegistrationDTO.Role = string.IsNullOrWhiteSpace(userRegistrationDTO.Role) || userRegistrationDTO.Role.Trim().ToLower() == "string"
                ? "User"
                : userRegistrationDTO.Role.Trim();

            var allowedRoles = new List<string> { "User", "Admin" };
            if (!allowedRoles.Contains(userRegistrationDTO.Role))
            {
                return BadRequest("Invalid role");
            }

            var userDTO = await _userService.AddUserService(userRegistrationDTO);
            if (userDTO == null)
            {
                return BadRequest("Failed to create user");
            }

            return Ok(userDTO);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO user)
        {
            var userDB = await _userService.GetByEmailUserService(user.Email);

            if (userDB == null)
            {
                return Unauthorized("User not found");
            }

            bool isValidPassword = _userService.VerifyPasswordUserService(user.Password, userDB);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            var tokenResponse = _tokenService.GenerateTokenService(user);
            return Ok(tokenResponse);
        }


        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userService.ChangePasswordUserService(changePasswordDTO.Email, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
            if (!result)
            {
                return BadRequest("Invalid credentials or user not found");
            }

            return Ok("Password changed successfully");
        }
    }
}

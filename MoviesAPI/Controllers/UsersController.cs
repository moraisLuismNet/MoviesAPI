﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTOs;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [ResponseCache(Duration = 20)]
        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            var users = await _userService.GetUserService();
            return Ok(users);
        }


        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            var result = await _userService.DeleteUserService(email);
            if (result == null)
            {
                return NotFound("User not found");
            }

            return NoContent();
        }
    }
}

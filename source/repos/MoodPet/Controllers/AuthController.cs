using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly Auth _authService;

        public AuthController(Auth authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            Usuario newUser = new Usuario()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Tel = registerUserDto.Phone,
                Email = registerUserDto.Email,
                Password = registerUserDto.Password
            };

            var result = await _authService.RegisterUser(newUser);
            if (result == false) return BadRequest("Usuario ya existe");

            return Ok("Registro exitoso");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(
                loginDto.Email,
                loginDto.Password
            );

            var role = await _authService.GetUserRole(loginDto.Email);
            

            return Ok(new { Token = result, Role = role});
        }


    }
}

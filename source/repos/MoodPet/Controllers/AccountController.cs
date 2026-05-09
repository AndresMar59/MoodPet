using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Infraestructure.Identity;
using MoodPetApi.DTOs.User;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public AccountController(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(user);
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            user.Email = dto.Email;
            if (dto.Lastname != null)
                user.Lastname = dto.Lastname;
            if (dto.Tel != null)
                user.PhoneNumber = dto.Tel;

            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!string.Equals(dto.NewPassword, dto.ConfirmNewPassword, StringComparison.Ordinal))
            {
                return Ok(new { message = "Contraseña nueva no coincide"});
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }
    }
}

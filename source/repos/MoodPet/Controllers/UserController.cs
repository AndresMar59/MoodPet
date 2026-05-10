using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPet.Infraestructure.Identity;
using MoodPetApi.DTOs.Mascota;
using MoodPetApi.DTOs.User;
using System.Security.Claims;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly MascotaService _mascotaService;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RazaService _razaService;

        public UserController(UserManager<AppIdentityUser> userManager, MascotaService mascotaService, RazaService razaService)
        {
            _userManager = userManager;
            _mascotaService = mascotaService;
            _razaService = razaService;
        }

        [HttpGet("razas")]
        public async Task<IActionResult> GetAllRazas()
        {
            var razas = await _razaService.GetAllRaza();

            if (razas == null || !razas.Any())
            {
                return NotFound("No se encontraron razas registradas.");
            }

            return Ok(razas);
        }

        [HttpPost("addMascota")]
        public async Task<IActionResult> AddMascota(AddMascotaDto mascotaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            // Si el userId no se proporciona en el DTO, lo obtenemos del token JWT
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (mascotaDto.userId == null)
            {
                mascotaDto.userId = userId;
            }

            try
            {
                var result = await _mascotaService.CreateMascotaAsync(new Mascota()
                {
                    CreaAt = DateTime.UtcNow,
                    IsDeleted = false,
                    Nombre = mascotaDto.nombre,
                    RazaId = mascotaDto.razaId,
                    FechaNacimiento = mascotaDto.fechaNacimiento,
                    Peso = mascotaDto.peso,
                    Sexo = mascotaDto.sexo,
                    UserId = mascotaDto.userId
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpGet("mascotas")]
        public async Task<IActionResult> GetMascotasByUsuario()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            try
            {
                var mascotas = await _mascotaService.GetAllMascotasByUsuario(userId);

                var dtos = mascotas.Select(m => new
                {
                    m.Id,
                    m.Nombre,
                    m.Edad,
                    m.Peso,
                    m.Sexo,
                    m.FechaNacimiento,

                    Raza = new
                    {
                        m.RazaId,
                        m.Raza.Nombre,

                        Especie = new
                        {
                            m.Raza.EspecieId,
                            m.Raza.Especie.Nombre
                        }
                    }
                });

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpPatch("updateMascota/{mascotaId:int}")]
        public async Task<IActionResult> Update(int mascotaId, MascotaPatchDto updateMascotaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var mascota = await _mascotaService.GetByID(mascotaId);

                // Actualizando las propiedades de la mascota

                if (updateMascotaDto.Nombre != null)
                    mascota.Nombre = updateMascotaDto.Nombre;

                if (updateMascotaDto.Peso.HasValue)
                    mascota.Peso = updateMascotaDto.Peso.Value;

                if (updateMascotaDto.Sexo != null)
                    mascota.Sexo = updateMascotaDto.Sexo;

                if (updateMascotaDto.FechaNacimiento.HasValue)
                    mascota.FechaNacimiento = updateMascotaDto.FechaNacimiento.Value;

                if (updateMascotaDto.RazaId.HasValue)
                    mascota.RazaId = updateMascotaDto.RazaId.Value;

                // Guardando los cambios
                var result = await _mascotaService.UpdateMascota(mascota);

                return Ok(new { message = result });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{mascotaId:int}")]
        public async Task<IActionResult> Delete(int mascotaId)
        {
            try
            {
                var mascota = await _mascotaService.GetByID(mascotaId);

                var result = await _mascotaService.DeleteMascotaById(mascotaId);

                return Ok(new { Success = result });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            return Ok(user);
        }

        [HttpPost("updateMe")]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            user.Email = dto.Email;
            user.Firstname = dto.Name;
            if (dto.Lastname != null)
                user.Lastname = dto.Lastname;
            if (dto.Tel != null)
                user.PhoneNumber = dto.Tel;

            await _userManager.UpdateAsync(user);
            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var user = await _userManager.GetUserAsync(User);

            if (!string.Equals(dto.NewPassword, dto.ConfirmNewPassword, StringComparison.Ordinal))
            {
                return Ok(new { message = "Contraseña nueva no coincide" });
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

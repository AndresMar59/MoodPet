using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Mascota;
using System.Security.Claims;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MascotasController : Controller
    {
        private readonly MascotaService _mascotaService;
        public MascotasController(MascotaService mascotaService)
        {
            _mascotaService = mascotaService;
        }

        [HttpPost("add")]
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

        [HttpGet("{mascotaId:int}")]
        public async Task<IActionResult> GetMascotaById(int mascotaId)
        {
            try
            {
                var mascota = await _mascotaService.GetByID(mascotaId);

                var dto = new
                {
                    mascota.Id,
                    mascota.Nombre,
                    mascota.Edad,
                    mascota.Peso,
                    mascota.Sexo,
                    mascota.FechaNacimiento,
                    mascota.UserId,

                    Raza = new
                    {
                        mascota.RazaId,
                        mascota.Raza.Nombre,

                        Especie = new
                        {
                            mascota.Raza.EspecieId,
                            mascota.Raza.Especie.Nombre
                        }
                    }
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // Este endpoint devuelve todas las mascotas del usuario autenticado
        [HttpGet("usuario")]
        public async Task<IActionResult> GetMascotasByUsuario()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            try
            {
                var mascotas = await _mascotaService.GetAllMascotasByUsuario(userId);

                var dtos = mascotas.Select(m => new // Lllama todas las relaciones de la mascota, raza y especie
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

        [HttpPatch("{mascotaId:int}")]
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




    }
}

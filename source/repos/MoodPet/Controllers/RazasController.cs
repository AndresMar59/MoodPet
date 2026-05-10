using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Raza;
using MoodPetApi.DTOs.Species;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RazasController : Controller
    {
        private readonly RazaService _razaService;
        public RazasController(RazaService razaService)
        {
            _razaService = razaService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRaza(AddRazaDto razaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var result = await _razaService.CreateRazaAsync(new Raza()
                {
                    CreaAt = DateTime.UtcNow,
                    IsDeleted = false,
                    Nombre = razaDto.Name,
                    EspecieId = razaDto.SpeciesId
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        
        [HttpGet("{razaId:int}")]
        [Authorize(Roles = "Admin,User")] // Ambos pueden ver
        public async Task<IActionResult> GetRazaById(int razaId)
        {
            try
            {
                var raza = await _razaService.FindByIdAsync(razaId);

                var dto = new
                {
                    raza.Id,
                    raza.Nombre,

                    Especie = new
                    {
                        raza.Especie.Id,
                        raza.Especie.Nombre
                    }
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{razaId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int razaId, AddRazaDto updateRazaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var raza = await _razaService.GetByID(razaId);

                // Actualizando las propiedades de la raza
                if (updateRazaDto.Name != null)
                    raza.Nombre = updateRazaDto.Name;

                // Guardando los cambios
                await _razaService.UpdateRazaAsync(raza);

                return Ok(new { message = "raza actualizada" });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{razaId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int razaId)
        {
            try
            {
                var especie = await _razaService.GetByID(razaId);

                var result = await _razaService.DeleteById(razaId);
               
                return Ok(new { Success = result });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



        [HttpGet("Especies/{especieId:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetRazasByEspecie(int especieId)
        {
            try
            {
                var razas = await _razaService.GetRazabyEspecies(especieId);

                var dtos = razas.Select(raza => new
                {
                    raza.Id,
                    raza.Nombre,
                    Especie = raza.Especie != null ? new
                    {
                        raza.Especie.Id,
                        raza.Especie.Nombre
                    } : null
                });

                return Ok(dtos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado", details = ex.Message });
            }
        }


    }
    }


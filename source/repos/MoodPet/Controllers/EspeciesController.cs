using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Species;
using System.Security.Claims;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EspeciesController : Controller
    {
        private readonly EspecieService _especieService;
        public EspeciesController(EspecieService especieService)
        {
            _especieService = especieService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecies(AddSpeciesDto speciesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var result = await _especieService.CreateSpeciesAsync(new Especie()
                {
                        CreaAt = DateTime.UtcNow,
                        IsDeleted = false,
                        Nombre = speciesDto.Name
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{speciesId:int}")]
        public async Task<IActionResult> GetSpeciesById(int speciesId)
        {
            try
            {
                var species = await _especieService.GetByID(speciesId);

                var dto = new
                {
                    species.Id,
                    species.Nombre,

                    razas = species.Razas.Where(r => !r.IsDeleted).Select(r => new
                    {
                        r.Id,
                        r.Nombre
                    })
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpPatch("{speciesId:int}")]
        public async Task<IActionResult> Update(int speciesId, AddSpeciesDto updateSpeciesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var species = await _especieService.GetByID(speciesId);

                // Actualizando las propiedades de la especie
                if (updateSpeciesDto.Name != null)
                    species.Nombre = updateSpeciesDto.Name;

                // Guardando los cambios
                await _especieService.UpdateEspecieAsync(species);

                return Ok(new { message = "especie actualizada" });

            }
            catch( Exception ex) 
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{speciesId:int}")]
        public async Task<IActionResult> Delete(int speciesId)
        {
            try
            {
                var especie = await _especieService.GetByID(speciesId);
           
                var result = await _especieService.DeleteById(speciesId);
                return Ok(new { Success = result });

            }
            catch ( Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Species;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TipoEventosController : Controller
    {
        private readonly TipoEventoService _tipoEventoService;
        public TipoEventosController(TipoEventoService tipoEventoService)
        {
            _tipoEventoService = tipoEventoService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTipoEvento(AddSpeciesDto tipoEventoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var result = await _tipoEventoService.CreateAsync(new TipoEvento()
                {
                    CreaAt = DateTime.UtcNow,
                    IsDeleted = false,
                    Name = tipoEventoDto.Name
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{tipoEventoId:int}")]
        public async Task<IActionResult> GetTipoEventoById(int tipoEventoId)
        {
            try
            {
                var tipoEvento = await _tipoEventoService.GetByID(tipoEventoId);

                var dto = new
                {
                    tipoEvento.Id,
                    tipoEvento.Name,
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpPatch("{tipoEventoId:int}")]
        public async Task<IActionResult> Update(int tipoEventoId, AddSpeciesDto updateSpeciesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var tipoEvento = await _tipoEventoService.GetByID(tipoEventoId);

                // Actualizando las propiedades de la especie
                if (updateSpeciesDto.Name != null)
                    tipoEvento.Name = updateSpeciesDto.Name;

                // Guardando los cambios
                await _tipoEventoService.UpdateTipoEventoAsync(tipoEvento);

                return Ok(new { message = "tipo de evento actualizado" });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{tipoEventoId:int}")]
        public async Task<IActionResult> Delete(int tipoEventoId)
        {
            try
            {
                var tipoEvento = await _tipoEventoService.GetByID(tipoEventoId);

                var result = await _tipoEventoService.DeleteById(tipoEventoId);
                return Ok(new { Success = result });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

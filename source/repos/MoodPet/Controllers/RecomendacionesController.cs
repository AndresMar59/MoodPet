using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecomendacionesController : Controller
    {

        private readonly RecomendacionService _recomendacionService;

        public RecomendacionesController(RecomendacionService recomendacionService)
        {
            _recomendacionService = recomendacionService;
        }

        [HttpPost("generarRecomendacion/{mascotaId:int}")]
        public async Task<IActionResult> GenerarRecomendacion(int mascotaId)
        {
            try
            {
                var recomendacion = await _recomendacionService.GenerarRecomendacionPorMascota(mascotaId); // recomendacion es del tipo HistorialRecomendacion
                return Ok(recomendacion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al generar la recomendación.", detalle = ex.Message });
            }
        }

        [HttpGet("historialRecomendacion/{mascotaId}")]
        public async Task<IActionResult> GetHistorial(int mascotaId)
        {
            var historial = await _recomendacionService.GetHistorialPorMascota(mascotaId);

            if (historial == null || !historial.Any())
            {
                return NotFound(new { mensaje = "Esta mascota todavía no tiene recomendaciones generadas." });
            }

            return Ok(historial);
        }
    }
}

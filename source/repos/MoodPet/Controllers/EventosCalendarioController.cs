using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces.Repositorio;
using MoodPetApi.DTOs.EventoCalendario;
using System.Security.Claims;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventosCalendarioController : ControllerBase
    {
         readonly EventoCalendarioService _eventoCalendario;
         readonly TipoEventoService _tipoEvento;

        public EventosCalendarioController(EventoCalendarioService eventoCalendarioService, TipoEventoService tipoEvento)
        {
            _eventoCalendario = eventoCalendarioService;
            _tipoEvento = tipoEvento;

        }

        [HttpGet("tipos-evento")]
        public async Task<IActionResult> GetTiposEvento()
        {
            var tipos = await _tipoEvento.GetAllTipoEvento();
            return Ok(tipos);
        }


        [HttpPost("creaEvento")]
        public async Task<IActionResult> CreateEventoCalendario(AddEventoCalendarioDto addEventoCalendarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            // Si el UserId no se proporciona en el DTO, se obtiene del token JWT
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (addEventoCalendarioDto.UserId == null)
            {
                addEventoCalendarioDto.UserId = userId;
            }

            try
            {
                var result = await _eventoCalendario.CreateEvento(new EventoCalendario()
                {
                    CreaAt = DateTime.UtcNow,
                    IsDeleted = false,
                    titulo = addEventoCalendarioDto.Titulo,
                    Descripcion = addEventoCalendarioDto.Descripcion,
                    FechaEvento = addEventoCalendarioDto.FechaEvento,
                    Estado = EventoCalendario.EstadoEvento.Pendiente,
                    TipoEventoId = addEventoCalendarioDto.TipoEventoId,
                    UserId = addEventoCalendarioDto.UserId,
                    MascotaId = addEventoCalendarioDto.MascotaId,

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("eventos")]
        public async Task<IActionResult> GetEventosByUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                var eventos = await _eventoCalendario.GetEventosByUserId(userId);
                var dtoList = eventos.Select(evento => new
                {
                    evento.Id,
                    evento.titulo,
                    evento.Descripcion,
                    evento.FechaEvento,
                    evento.Estado,
                    Mascota = new
                    {
                        evento.Mascota.Id,
                        evento.Mascota.Nombre
                    },
                    TipoEvento = new
                    {
                        evento.TipoEvento.Id,
                        evento.TipoEvento.Name
                    }
                });
                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPatch("actualizarEvento/{EventoId:int}")]
        public async Task<IActionResult> UpdateEventoCalendario(int EventoId, UpdateEventoCalendarioDto updateEventoCalendarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var evento = await _eventoCalendario.GetByID(EventoId);

                // Actualizar las propiedades del evento
                if (updateEventoCalendarioDto.Descripcion != null)
                    evento.Descripcion = updateEventoCalendarioDto.Descripcion;

                if (updateEventoCalendarioDto.FechaEvento.HasValue)
                    evento.FechaEvento = updateEventoCalendarioDto.FechaEvento.Value;

                if (updateEventoCalendarioDto.Estado.HasValue)
                    evento.Estado = updateEventoCalendarioDto.Estado.Value;

                if (updateEventoCalendarioDto.TipoEventoId.HasValue)
                    evento.TipoEventoId = updateEventoCalendarioDto.TipoEventoId.Value;

                var result = await _eventoCalendario.UpdateEvento(evento);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("eliminarEvento/{EventoId:int}")]
        public async Task<IActionResult> CancelarEventoCalendario(int EventoId)
        {
            try
            {
                var result = await _eventoCalendario.MarkEventoAsCancelled(EventoId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet("{EventoId:int}")]
        public async Task<IActionResult> GetEventoById(int EventoId)
        {
            try
            {
                var evento = await _eventoCalendario.GetByID(EventoId);

                var dto = new
                {
                    evento.Id,
                    evento.Descripcion,
                    evento.FechaEvento,
                    evento.Estado,

                    Mascota = new
                    {
                        evento.Mascota.Id,
                        evento.Mascota.Nombre
                    },

                    TipoEvento = new
                    {
                        evento.TipoEvento.Id,
                        evento.TipoEvento.Name
                    }
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}

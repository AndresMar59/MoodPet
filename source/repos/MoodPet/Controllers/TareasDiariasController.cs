using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Mascota;
using MoodPetApi.DTOs.Tarea;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TareasDiariasController : Controller
    {
        private readonly TareaService _tareaService;
        public TareasDiariasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost("crearTarea")]
        public async Task<IActionResult> AddTarea(TareaDTO tareaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var result = await _tareaService.CreateAsync(new TareaDiaria()
                {
                    
                    Titulo = tareaDto.Titulo,
                    Descripcion = tareaDto.Descripcion,
                    Fecha = tareaDto.Fecha,
                    Recurrente = tareaDto.Recurrente,
                    Semanas = tareaDto.Semanas ?? (tareaDto.Recurrente ? 12 : 1),
                    Hora = tareaDto.Hora,
                    MascotaId = tareaDto.MascotaId
                });
                if(result.Contains("completada")) return Ok(result);
                else return BadRequest(result);
               
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        //Devuelve todas las tareas por mascota
        [HttpGet("AllTareasMascota/{Id:int}")]
        public async Task<IActionResult> GetAllTareasByMascotas(int Id)
        {

            try
            {
                var Tarea = await _tareaService.GetAllbyMascot(Id);
                return Ok(Tarea);
            }


            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpDelete("toComplete/{Id:int}")]
        public async Task<IActionResult> CompleteTarea(int Id)
        {
            try
            {

                var result = await _tareaService.deleteById(Id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



















        [HttpPatch("historial/{historialId:int}/tarea/{id:int}")]
        public async Task<IActionResult> UpdateToComplete(int historialId, int id)
        {
            try
            {
                var complete = await _tareaService.CompleteTareaByHistorial(historialId, id);

                return Ok(new { success = complete, message = "Estado actualizado correctamente" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        

        
        
        [HttpPatch("{id:int}/semana/{semana:int}")]
        public async Task<IActionResult> CompletebySemana(int id, int semana)
        {
            try
            {
                var result = await _tareaService.CompleteTareaBySemana(semana, id);
                return Ok(new { message = "Tarea actualizada", data = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno", details = ex.Message });
            }
        }

        
        
        
        [HttpGet("Mascota/{Id:int}")]
        public async Task<IActionResult> GetTareabyMascota(int Id)
        {

            try
            {
                var Tarea = await _tareaService.FindByMascota(Id);
                return Ok(Tarea);
            }


            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }



        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetTareaById(int Id)
        {
            try
            {
                var Tarea = await _tareaService.GetById(Id);
                return Ok(Tarea);
            }


            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpGet("GetAllHistorial/{Id:int}")]
        public async Task<IActionResult> GetHistorialByTarea(int Id)
        {
            try
            {
                var historial = await _tareaService.GetHistorialByTarea(Id);
                return Ok(historial);
            }
            catch (Exception ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }






    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoodPet.Application.Service;
using MoodPet.Domain.Entities;
using MoodPetApi.DTOs.Raza;
using MoodPetApi.DTOs.Species;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        // =====================================================
        // RAZAS
        // =====================================================

        // GET: api/Admin/razas
        [HttpGet("razas")]
        public async Task<IActionResult> GetAllRazas()
        {
            var razas = await _adminService.GetAllRaza();

            if (razas == null || !razas.Any())
            {
                return NotFound("No se encontraron razas registradas.");
            }

            return Ok(razas);
        }

        // POST: api/Admin/razas
        [HttpPost("razas")]
        public async Task<IActionResult> CreateRaza(AddRazaDto addRazaDto)
        {
            if (addRazaDto == null)
            {
                return BadRequest("Los datos de la raza no pueden estar vacíos.");
            }
            var new_raza = new Raza()
            {
                Nombre = addRazaDto.Name,
                EspecieId = addRazaDto.SpeciesId
            };

            var result = await _adminService.CreateRazaAsync(new_raza);

            if (result.Contains("no existe"))
            {
                return BadRequest(result);
            }

            if (result.Contains("ya existe"))
            {
                return Conflict(result);
            }

            return Ok(result);
        }

        // PUT: api/Admin/razas/5
        [HttpPut("razas/{id:int}")]
        public async Task<IActionResult> UpdateRaza(int id, [FromBody] AddRazaDto updateRazaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var raza = await _adminService.GetRazaByID(id);

                // Actualizando las propiedades de la raza
                if (updateRazaDto.Name != null)
                    raza.Nombre = updateRazaDto.Name;
                if (updateRazaDto.SpeciesId > 0)
                    raza.EspecieId = updateRazaDto.SpeciesId;

                // Guardando los cambios
                await _adminService.UpdateRazaAsync(raza);

                return Ok(new { message = "raza actualizada" });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/Admin/razas/5
        [HttpDelete("razas/{id:int}")]
        public async Task<IActionResult> DeleteRaza(int id)
        {
            try
            {
                var result = await _adminService.DeleteRazaById(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // =====================================================
        // ESPECIES
        // =====================================================

        // GET: api/Admin/especies
        [HttpGet("especies")]
        public async Task<IActionResult> GetAllEspecies()
        {
            var especies = await _adminService.GetAllEspecies();

            if (especies == null || !especies.Any())
            {
                return NotFound("No se encontraron especies registradas.");
            }

            return Ok(especies);
        }

      

        // POST: api/Admin/especies
        [HttpPost("especies")]
        public async Task<IActionResult> CreateEspecie(AddSpeciesDto speciesDto)
        {
            if (speciesDto == null)
            {
                return BadRequest("Los datos de la especie no pueden estar vacíos.");
            }

            var new_especie = new Especie()
            {
                Nombre = speciesDto.Name
            };

            var result = await _adminService.CreateSpeciesAsync(new_especie);

            if (result.Contains("ya existe"))
            {
                return Conflict(result);
            }

            return Ok(result);
        }

        // PUT: api/Admin/especies/5
        [HttpPut("especies/{id:int}")]
        public async Task<IActionResult> UpdateEspecie(int id, [FromBody] AddSpeciesDto especie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Datos insuficientes" });
            }

            try
            {
                var species = await _adminService.GetEspecieByID(id);

                // Actualizando las propiedades de la especie
                if (especie.Name != null)
                    species.Nombre = especie.Name;

                // Guardando los cambios
                await _adminService.UpdateEspecieAsync(species);

                return Ok(new { message = "especie actualizada" });

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/Admin/especies/5
        [HttpDelete("especies/{id:int}")]
        public async Task<IActionResult> DeleteEspecie(int id)
        {
            try
            {
                var result = await _adminService.DeleteEspecieById(id);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

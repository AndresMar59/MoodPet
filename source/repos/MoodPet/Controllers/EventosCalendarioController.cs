using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EventosCalendarioController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(new { message = "Eventos del calendarioxd" });
        }
    }
}

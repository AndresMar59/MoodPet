using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class EventosCalendarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

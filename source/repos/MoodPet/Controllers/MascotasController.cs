using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class MascotasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

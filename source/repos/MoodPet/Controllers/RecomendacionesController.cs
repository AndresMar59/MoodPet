using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class RecomendacionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

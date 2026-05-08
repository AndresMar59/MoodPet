using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class TareasDiariasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

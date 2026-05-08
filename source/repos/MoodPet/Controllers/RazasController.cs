using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class RazasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class EspeciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

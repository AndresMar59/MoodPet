using Microsoft.AspNetCore.Mvc;

namespace MoodPetApi.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Msit155Site.Controllers
{
    public class HomeworkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}

using Microsoft.AspNetCore.Mvc;

namespace POOII_cibertec_demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (TempData["Msg"] != null)
                ViewBag.Mensaje = TempData["Msg"];
            return View();
        }
    }
}

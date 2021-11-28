using Microsoft.AspNetCore.Mvc;

namespace z_AdminLTE.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

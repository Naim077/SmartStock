using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

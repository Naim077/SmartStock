using Microsoft.AspNetCore.Mvc;

namespace AIMS.Web.Ui.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }
    }
}

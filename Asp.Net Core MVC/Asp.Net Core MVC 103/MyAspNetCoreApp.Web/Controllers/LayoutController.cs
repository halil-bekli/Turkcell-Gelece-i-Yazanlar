using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp3.Web.Controllers {
    public class LayoutController : Controller {
        public IActionResult CustomLayout() {
            return View();
        }

        public IActionResult NoLayout() {
            return View();
        }
    }
}

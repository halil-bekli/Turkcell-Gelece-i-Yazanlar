using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp2.Web.Controllers {
    public class FirstController : Controller {

        // All of the Action Method Return types:
        public IActionResult Index() {
            // Data transportation to view methods
            ViewBag.Message = "Login";

            ViewBag.Array = new string[3] { "5", "abcd", "halil" };

            ViewData["age"] = 19;

            ViewData["names"] = new List<string> { "ahmet", "hatice", "halil" };

            ViewBag.person = new {
                Id = 1,
                name = "Ahmet",
                age = 24
            };

            TempData["soyadi"] = "yılmaz"; // if we want to transfer some data to another controller (so view). it is kept on the cookies
            TempData.Keep("soyadi"); // /**********/ to load the temp data it have to run this Index Action method 

            return View(); // only if we return a View() in a Act Method then we need a View in the Views
        }

        public IActionResult ShowTempData() {
            var surName = TempData["soyadi"]; // after run of tempdata's action method

            return View();
        }

        public IActionResult ContentResult() { // content içindeki mesajın gösterildiği bir sayfa oluşturur
            return Content("Result");
        }

        public IActionResult JsonResult() {
            return Json(new { id = 1, name = "kalem 1", price = 100 });
        }

        public IActionResult EmptyResult() {
            return new EmptyResult();
        }
    }
}

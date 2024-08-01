using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp2.Web.Controllers {
    public class RedirectionController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult SignUp() {
            // örneğin veri tabanı kaydetme işlemi
            ViewBag.Message = "Login"; // Burada ViewBag den sonra gelen isim dinamik olarak bir değişken oluşturur. //
            return RedirectToAction("Index"); // sonrasında "işlemin başarılı olduğunu" belirten bir sayfaya yöneltebiliriz
        }

        public IActionResult Login() {
            return RedirectToAction("Index", "First");
        }

        public IActionResult ParameterView(int id) { // url den girilir
            return RedirectToAction("JsonResultParameter", new { idJson = id }); // idJsonisimli parametresi olan fonk. a yönlendirme
        }

        public IActionResult JsonResultParameter(int idJson) {
            return Json(new { Id = idJson });
        }
    }
}

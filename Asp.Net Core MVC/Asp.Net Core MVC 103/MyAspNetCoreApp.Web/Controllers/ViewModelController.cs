using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp3.Web.Controllers {

    public class Products {

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ViewModelController : Controller {
        public IActionResult Index() {
            Products product = new() { Id = 1, Name = "Kitap" };

            var productList = new List<Products>() {
                new() { Id = 2, Name = "Kalem"},
                new() { Id = 3, Name = "Defter" },
                new() { Id = 4, Name = "Tahta"}
            };

            productList.Add(product);

            return View(productList);
        }
    }
}

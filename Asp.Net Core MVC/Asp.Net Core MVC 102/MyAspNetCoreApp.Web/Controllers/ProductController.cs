using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp2.Web.Helpers;
using MyAspNetCoreApp2.Web.Models;

namespace MyAspNetCoreApp2.Web.Controllers {
    public class ProductController : Controller {

        private readonly ProductRepository _productRepository;

        private AppDbContext _context;

        private IHelper _helper;

        public ProductController(AppDbContext context, IHelper helper) { // constr injection -> program.cs

            //DI Container : constructor da DbContext sınıfı gördüğünde otomatik olarak bir DbContext örneği oluşturuyor
            //Dependency Injection Pattern
            _context = context;

            _helper = helper;

            //                 Linq method
            //if (!_context.Products.Any()) {
            //    _context.Products.Add(new Product { Name = "kalem", Price = 100, Stock = 100, Color = "Red", Height = 1 });
            //    _context.Products.Add(new Product { Name = "kalem2", Price = 200, Stock = 100, Color = "Red", Height = 1 });
            //    _context.Products.Add(new Product { Name = "kalem3", Price = 300, Stock = 100, Color = "Red", Height = 1 });

            //    _context.SaveChanges();
            //}

            _productRepository = new ProductRepository();
            /*
            if (!_productRepository.GetProducts().Any()) {

            }*/
        }

        [HttpGet] // <--- deafult
        public IActionResult Index([FromServices] IHelper helper2) { // method injection

            var text = "asp.net";
            var upperText = _helper.Upper(text);

            var status = _helper.Equals(helper2);


            var products = _context.Products.ToList(); //_productRepository.GetProducts();

            return View(products);
        }

        public IActionResult Remove(int id) {
            var product = _context.Products.Find(id); //verilen değeri (id) primary key de arar, kayıt varsa döndürür

            _context.Products.Remove(product);
            _context.SaveChanges(); // değişiklikleri Db e kaydet

            return RedirectToAction("Index");
        }

        [HttpGet] // http, alır. url'de yapılan tüm işlemler
        public IActionResult aAdd() {

            return View();
        }

        [HttpPost] // http, back end e gönderir. sayfadan gönderilen tüm istekler. veriler, gelen requestlerin body kısmında (güvenli) gelir.

        // Getting the datas from form -> (Model Binding)
        public IActionResult Add(string Name, decimal Price, int Stock, string Color) {  // 2. Method: eğer formadan gelecek (input) verilerin

            // tiplerini parametre olarak verirsek otomatik olarak type casting yapılır
            // Request: Header-Body

            //// 1. Method 
            //var name = HttpContext.Request.Form["Name"].ToString(); // key => name (input tag) 
            //var price = Convert.ToDecimal(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            Product product = new() { Name = Name, Price = Price, Stock = Stock, Color = Color };

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla eklendi";

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Add2(Product newProduct) {  // 3. Method: class model oluşturarak verileri alma

            _context.Products.Add(newProduct);
            _context.SaveChanges();


            return RedirectToAction("add");
        }

        [HttpGet]
        public IActionResult SaveProductWithHttpGet(Product newProduct) {  // 4. Method: hhtp get ile, class model oluşturarak verileri alma
            // değerler url'de görünür (url string ile taşınır). Sınırlı data... -> Güvenli değil
            //https://localhost:xxxxx/product/SaveProductWithHttpGet?Name=makas&Price=134&Stock=1123&Color=blue

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return View();
        }

        [HttpGet]
        public IActionResult Update(int id) {

            var product = _context.Products.Find(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product updateProduct) {

            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi";

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult HybridUpdate(Product updateProduct, int productId) {

            updateProduct.Id = productId;
            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla güncellendi";

            return RedirectToAction("index");
        }
    }
}

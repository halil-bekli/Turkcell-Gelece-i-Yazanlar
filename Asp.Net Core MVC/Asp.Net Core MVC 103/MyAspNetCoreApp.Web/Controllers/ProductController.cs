using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp3.Web.Helpers;
using MyAspNetCoreApp3.Web.Models;
using MyAspNetCoreApp3.Web.ViewModels;

namespace MyAspNetCoreApp3.Web.Controllers {
    public class ProductController : Controller {

        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;

        private AppDbContext _context;

        private IHelper _helper;

        public ProductController(AppDbContext context, IHelper helper, IMapper mapper) { // constr injection -> program.cs

            //DI Container : constructor da DbContext sınıfı gördüğünde otomatik olarak bir DbContext örneği oluşturuyor
            //Dependency Injection Pattern
            _context = context;

            _helper = helper;

            _mapper = mapper;

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
        public IActionResult Index() { // method injection

            var products = _context.Products.ToList(); //_productRepository.GetProducts();

            return View(_mapper.Map<List<ProductViewModel>>(products));
        }

        public IActionResult Remove(int id) {
            var product = _context.Products.Find(id); //verilen değeri (id) primary key de arar, kayıt varsa döndürür

            _context.Products.Remove(product);
            _context.SaveChanges(); // değişiklikleri Db e kaydet

            return RedirectToAction("Index");
        }

        [HttpGet] // http, alır. url'de yapılan tüm işlemler
        public IActionResult Add() {

            Dictionary<string, int> expire = new Dictionary<string, int>() {
                {"1 ay", 1}, {"3 ay", 3}, {"6 ay", 6}, {"12 ay", 12}
            };

            ViewBag.Expire = expire;

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList> {
                new() {Data = "Red", Value = "Red"},
                new() {Data = "Blue", Value = "Blue"},
                new() {Data = "Yellow", Value = "Yellow"}
                }, "Value", "Data");

            return View();
        }

        [HttpPost] // http, back end e gönderir. sayfadan gönderilen tüm istekler. veriler, gelen requestlerin body kısmında (güvenli) gelir.

        // Getting the datas from form -> (Model Binding)
        public IActionResult Add(string Name, decimal Price, int Stock, string Color, bool IsPublish, int Expire) {  // 2. Method: eğer formadan gelecek (input) verilerin

            // tiplerini parametre olarak verirsek otomatik olarak type casting yapılır
            // Request: Header-Body

            //// 1. Method 
            //var name = HttpContext.Request.Form["Name"].ToString(); // key => name (input tag) 
            //var price = Convert.ToDecimal(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();

            Product product = new() { Name = Name, Price = Price, Stock = Stock, Color = Color, IsPublish = IsPublish, Expire = Expire };

            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["status"] = "Ürün başarıyla eklendi";

            return RedirectToAction("index");
        }

        [HttpPost]
        public IActionResult Add2(ProductViewModel newProduct) {  // 3. Method: class model oluşturarak verileri alma

            if (ModelState.IsValid) {
                _context.Products.Add(_mapper.Map<Product>(newProduct));
                _context.SaveChanges();

                TempData["status"] = "Ürün başarıyla eklendi";
                return RedirectToAction("Index");

            } else {

                Dictionary<string, int> expire = new Dictionary<string, int>() {
                {"1 ay", 1}, {"3 ay", 3}, {"6 ay", 6}, {"12 ay", 12}
                };

                ViewBag.Expire = expire;

                ViewBag.ColorSelect = new SelectList(new List<ColorSelectList> {
                new() {Data = "Red", Value = "Red"},
                new() {Data = "Blue", Value = "Blue"},
                new() {Data = "Yellow", Value = "Yellow"}
                }, "Value", "Data");

                return View("Add");
            }
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

            ViewBag.radioExpireValue = product.Expire;

            ViewBag.Expire = new Dictionary<string, int>() {
                {"1 ay", 1}, {"3 ay", 3}, {"6 ay", 6}, {"12 ay", 12}
            };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList> {
                new() {Data = "Red", Value = "Red"},
                new() {Data = "Blue", Value = "Blue"},
                new() {Data = "Yellow", Value = "Yellow"}
            }, "Value", "Data", product.Color);

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

namespace MyAspNetCoreApp.Web.Models {
    public class ProductRepository {
        private static List<Product> _products = new();

        static ProductRepository() {
            _products.Add(new() { Id = 1, Name = "Phone", Price = 100, Stock = 100 });
            _products.Add(new() { Id = 2, Name = "Phone 2", Price = 500, Stock = 168 });
            _products.Add(new() { Id = 3, Name = "Phone 3", Price = 1000, Stock = 17 });
        }

        public List<Product> GetProducts() => _products; // == return _products;

        public void AddProduct(Product newProduct) => _products.Add(newProduct);

        public void UpdateProduct(Product updateProduct) {
            var hasProduct = _products.FirstOrDefault(x => updateProduct.Id == x.Id);

            if (hasProduct == null) {
                throw new Exception($"Bu ID({updateProduct.Id}) de bir ürün yok.");

            } else {

                hasProduct.Id = updateProduct.Id;
                hasProduct.Name = updateProduct.Name;
                hasProduct.Stock = updateProduct.Stock;

                int indexOfProduct = _products.IndexOf(hasProduct);
                _products[indexOfProduct] = hasProduct;
            }
        }

        public void RemoveProduct(int id) {
            var hasProduct = _products.FirstOrDefault(x => x.Id == id);

            if (hasProduct == null) {
                throw new Exception($"Bu ID({id}) de bir ürün yok.");

            } else {
                _products.Remove(hasProduct);
            }
        }


    }
}

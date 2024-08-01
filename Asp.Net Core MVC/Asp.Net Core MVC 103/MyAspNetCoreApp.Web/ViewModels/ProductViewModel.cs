using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp3.Web.ViewModels {
    public class ProductViewModel {

        public int Id { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        [StringLength(50, ErrorMessage = "isim alanına en fazla 50 karakter girilebilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        [Range(1, 1000, ErrorMessage = "Fiyat alanı 1 ile 1.000 arasında bir değer olmalıdır.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        [Range(1, 200, ErrorMessage = "Stok alanı 1 ile 200 arasında olmalıdır.")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description alanına 10 ile 500 karakter girilebilir.")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Boş bırakılamaz.")]
        public string? Color { get; set; }

        public int? Height { get; set; }

        //public int? Width { get; set; }
        public string? Barcode { get; set; }

        public bool IsPublish { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        public int? Expire { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        public DateTime? PublishDate { get; set; }

    }
}